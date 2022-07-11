package io.sentry.examples.springwebfluxtunnel;

import com.fasterxml.jackson.databind.DeserializationFeature;
import com.fasterxml.jackson.databind.JsonNode;
import com.fasterxml.jackson.databind.ObjectMapper;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.http.HttpHeaders;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.reactive.function.client.ExchangeStrategies;
import org.springframework.web.reactive.function.client.WebClient;
import org.springframework.web.reactive.function.client.WebClientResponseException;

import java.io.IOException;
import java.net.URI;
import java.nio.charset.StandardCharsets;
import java.time.Duration;
import java.util.Set;

import lombok.extern.slf4j.Slf4j;
import reactor.core.publisher.Mono;
import reactor.util.retry.Retry;

/**
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
      throwable ->
        throwable instanceof IOException || throwable instanceof WebClientResponseException
    )
    .doBeforeRetry(
      retrySignal -> log.info("Retrying sentry request... {}", retrySignal.getClass())
    );

  public SentryTunnelController(
    @Value("${sentry.tunnel-path}") String tunnelPath,
    @Value("${sentry.url}") String sentryUrl,
    @Value("#{'${sentry.allowed-project-ids}'.split(',')}") Set<Integer> allowedProjectIds
  ) {
    log.info("Creating a sentry tunnel controller with tunnel-path {}", tunnelPath);

    final int size = 16 * 1024 * 1024;
    final ExchangeStrategies strategies = ExchangeStrategies
      .builder()
      .codecs(codecs -> codecs.defaultCodecs().maxInMemorySize(size))
      .build();
    this.client = WebClient.builder().baseUrl(sentryUrl).exchangeStrategies(strategies).build();
    this.objectMapper = new ObjectMapper();
    this.allowedProjectIds = allowedProjectIds;
    objectMapper.configure(DeserializationFeature.FAIL_ON_UNKNOWN_PROPERTIES, false);
  }

  @PostMapping("${sentry.tunnel-path}")
  public Mono<ResponseEntity<Void>> tunnel(@RequestBody byte[] envelope) {
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
      Integer projectId = Integer.parseInt(uri.getPath().replaceAll("/$", ""));

      if (!allowedProjectIds.contains(projectId)) {
        return Mono.error(() -> new IllegalArgumentException("ProjectID not allowed " + projectId));
      }

      log.info("Forwarding with dsn {}", dsn);

      return client
        .post()
        .uri("/api/{projectId}/envelope/", projectId)
        .header(HttpHeaders.CONTENT_TYPE, "application/x-sentry-envelope")
        .bodyValue(envelope)
        .retrieve()
        .toBodilessEntity()
        .retryWhen(SENTRY_RETRY);
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
