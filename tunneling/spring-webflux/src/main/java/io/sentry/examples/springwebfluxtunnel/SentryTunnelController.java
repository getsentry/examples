package io.sentry.examples.springwebfluxtunnel;


import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.exception.ExceptionUtils;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.HttpHeaders;
import org.springframework.http.ResponseEntity;
import org.springframework.http.client.reactive.ReactorClientHttpConnector;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.reactive.function.client.ExchangeStrategies;
import org.springframework.web.reactive.function.client.WebClient;
import org.springframework.web.reactive.function.client.WebClientResponseException;

import java.io.IOException;
import java.net.URI;
import java.nio.charset.StandardCharsets;
import java.time.Duration;
import java.util.Map;
import java.util.Set;

import io.netty.handler.logging.LogLevel;
import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;
import reactor.netty.http.client.HttpClient;
import reactor.netty.transport.logging.AdvancedByteBufFormat;
import reactor.util.retry.Retry;

/**
 * To add this controller to your app, simply expose it as a bean:
 *
 * <pre>
 *
 *  {@literal @}Bean
 *   public SentryTunnelController sentryTunnelController(
 *     {@literal @}Value("${sentry.tunnel-path}") String tunnelPath,
 *     {@literal @}Value("${sentry.url}") String sentryUrl,
 *     {@literal @}Value("#{'${sentry.allowed-project-ids}'.split(',')}") Set<Integer> allowedProjectIds
 *   ) {
 *     return new SentryTunnelController(tunnelPath, sentryUrl, allowedProjectIds);
 *   }
 *
 * </pre>
 *
 * When adding this controller to your application, you will probably need to adjust the security
 * configuration:
 *
 * <pre>
 *
 *   private @Value("${sentry.tunnel-path}") String tunnelPath;
 *
 *   public void configure(HttpSecurity httpSecurity) throws Exception {
 *     // ...
 *     httpSecurity.antMatchers(HttpMethod.POST, tunnelPath).permitAll()
 *     .and()
 *     .csrf(csrf -> {
 *       // ...
 *       csrf.ignoringAntMatchers(tunnelPath);
 *     })
 *   }
 * </pre>
 *
 *
 * @see "https://docs.sentry.io/platforms/javascript/troubleshooting/"
 */
@Slf4j
@RestController
public class SentryTunnelController {

  private final ObjectMapper objectMapper;
  private final WebClient client;
  private final Set<Integer> allowedProjectIds;

  // We really should never see a sentry post request with more than 2 MB
  private static final long MAX_ENVELOPE_SIZE = 1024 * 1024 * 2;

  public static final Retry SENTRY_RETRY = Retry
      .backoff(60, Duration.ofMillis(1000))
      .jitter(0.2)
      .filter(
          throwable -> {
            boolean isRetryable =
                throwable instanceof IOException || throwable instanceof WebClientResponseException;

            if (throwable instanceof WebClientResponseException) {
              WebClientResponseException responseException = (WebClientResponseException) throwable;
              log.debug("Sentry response body: {}", responseException.getResponseBodyAsString());
              responseException
                  .getHeaders()
                  .toSingleValueMap()
                  .forEach(
                      (key, value) -> log.trace("Response header: {}: {}", key, value)
                  );
              if (
                  responseException.getStatusCode().is4xxClientError() ||
                  responseException.getStatusCode().is3xxRedirection()
              ) {
                log.debug(
                    "Sentry request not retryable, response code is {}",
                    responseException.getRawStatusCode()
                );
                return false;
              }
            }

            return isRetryable;
          }
      )
      .doBeforeRetry(
          retrySignal -> {
            String failureString = null;
            Throwable failure = retrySignal.failure();

            if (failure != null) {
              Throwable rootCause = ExceptionUtils.getRootCause(failure);
              if (rootCause != null) {
                failureString = rootCause.getMessage();
              }
            }

            if (failureString == null) {
              failureString = "unknown";
            }

            log.info(
                "Retry # {} sentry request... {}, failure was {}",
                retrySignal.totalRetries(),
                retrySignal.getClass(),
                failureString
            );
          }
      );

  public SentryTunnelController(
      @Value("${sentry.tunnel-path}") String tunnelPath,
      @Value("${sentry.url}") String sentryUrl,
      @Value("#{'${sentry.allowed-project-ids}'.split(',')}") Set<Integer> allowedProjectIds
  ) {
    log.info(
        "Creating a sentry tunnel controller with tunnel-path {} and allowed-project-ids {}",
        tunnelPath,
        allowedProjectIds
    );

    final int size = 16 * 1024 * 1024;
    final ExchangeStrategies strategies = ExchangeStrategies
        .builder()
        .codecs(codecs -> codecs.defaultCodecs().maxInMemorySize(size))
        .build();

    HttpClient httpClient = HttpClient
        .create()
        .wiretap(
            "reactor.netty.http.client.HttpClient",
            LogLevel.DEBUG,
            AdvancedByteBufFormat.TEXTUAL
        );

    this.client =
        WebClient
            .builder()
            .baseUrl(sentryUrl)
            .clientConnector(new ReactorClientHttpConnector(httpClient))
            .exchangeStrategies(strategies)
            .build();

    this.objectMapper = new ObjectMapper();
    this.allowedProjectIds = allowedProjectIds;
    objectMapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);
  }

  @PostMapping("${sentry.tunnel-path}")
  public Mono<ResponseEntity<Object>> tunnel(
      @RequestBody byte[] envelope,
      @RequestHeader Map<String, String> headers
  ) {
    try {
      if (envelope.length > MAX_ENVELOPE_SIZE) {
        return Mono.error(
            () -> new IllegalArgumentException("Envelope is larger than " + MAX_ENVELOPE_SIZE)
        );
      }

      // the body the sentry client sends here is a 3 line string, each line contains a json string
      String envelopeAsString = new String(envelope, StandardCharsets.UTF_8);
      String[] parts = envelopeAsString.split("\n");

      if (parts.length == 0) {
        log.info("Could not split input body {}", envelopeAsString);
        return Mono.error(() -> new IllegalArgumentException("Could not split input body"));
      }

      String piece = parts[0];
      JsonNode header = objectMapper.readTree(piece);
      JsonNode dsnNode = header.get("dsn");

      if (dsnNode == null) {
        log.info("DSN not present in header {}", envelopeAsString);
        return Mono.error(
            () -> new IllegalArgumentException("DNS node not present in sentry envelope")
        );
      }

      String dsn = dsnNode.textValue();

      if (dsn == null) {
        log.info("Could not parse DSN '{}' from envelope {}", piece, envelopeAsString);
        return Mono.error(() -> new IllegalArgumentException("Could not parse DSN"));
      }

      URI uri = URI.create(dsn);
      Integer projectId = Integer.parseInt(
          StringUtils.removeEnd(uri.getPath(), "/").replace("/", "")
      );

      if (!allowedProjectIds.contains(projectId)) {
        return Mono.error(() -> new IllegalArgumentException("ProjectID not allowed " + projectId));
      }

      log.debug("Forwarding with dsn {}", dsn);

      return client
          .post()
          .uri("/api/{projectId}/envelope/", projectId)
          .headers(
              httpHeaders -> {
                headers.forEach(
                    (headerName, headerValue) -> {
                      if (
                          HttpHeaders.REFERER.equals(headerName) || HttpHeaders.ORIGIN.equals(headerName)
                      ) {
                        log.trace("Forwarding header {} with value {}", headerName, headerValue);
                        httpHeaders.add(headerName, headerValue);
                      } else {
                        log.trace("Not Forwarding header {} with value {}", headerName, headerValue);
                      }
                    }
                );
              }
          )
          .header(HttpHeaders.CONTENT_TYPE, "application/x-sentry-envelope")
          .bodyValue(envelope)
          .retrieve()
          .toBodilessEntity()
          .retryWhen(SENTRY_RETRY)
          .flatMap(
              voidResponseEntity -> {
                log.trace(
                    "Sentry request forwarded, response status: {}",
                    voidResponseEntity.getStatusCode()
                );
                return Mono.just(ResponseEntity.status(voidResponseEntity.getStatusCode()).build());
              }
          )
          .doOnError(throwable -> log.warn(throwable.getMessage(), throwable));
    } catch (Exception e) {
      log.info(
          "Sentry tunnel error: '{}' on envelope '{}'" + e.getMessage(),
          new String(envelope, StandardCharsets.UTF_8),
          e
      );
      return Mono.error(e);
    }
  }
}
