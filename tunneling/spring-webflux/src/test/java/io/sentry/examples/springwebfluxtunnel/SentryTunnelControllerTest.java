package io.sentry.examples.springwebfluxtunnel;

import com.google.common.collect.Sets;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockserver.integration.ClientAndServer;
import org.mockserver.junit.jupiter.MockServerExtension;
import org.mockserver.junit.jupiter.MockServerSettings;
import org.mockserver.matchers.Times;
import org.mockserver.model.HttpRequest;
import org.mockserver.model.HttpResponse;
import org.mockserver.verify.VerificationTimes;
import org.springframework.core.io.ClassPathResource;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpMethod;
import org.springframework.http.HttpStatus;

import java.io.IOException;
import java.util.HashMap;
import java.util.HashSet;
import java.util.Map;

import reactor.test.StepVerifier;

import static org.mockserver.model.HttpRequest.request;

@ExtendWith(MockServerExtension.class)
@MockServerSettings(ports = { 9000 })
class SentryTunnelControllerTest {

  private final ClientAndServer client;

  public SentryTunnelControllerTest(ClientAndServer client) {
    this.client = client;
  }

  @BeforeEach
  public void setup() {
    client.reset();
  }

  @Test
  void testForwarding() throws IOException {
    byte[] envelope = new ClassPathResource("/testbody.http").getInputStream().readAllBytes();

    HttpRequest request = request()
      .withMethod(HttpMethod.POST.name())
      .withHeader(HttpHeaders.REFERER, "https://google.com")
      .withPath("/api/4/envelope/")
      .withBody(envelope);

    client.when(request).respond(HttpResponse.response().withStatusCode(HttpStatus.OK.value()));

    String path = "/telemetry";
    String url = "http://localhost:9000";
    HashSet<Integer> allowedProjectIds = Sets.newHashSet(1, 2, 4);
    SentryTunnelController controller = new SentryTunnelController(path, url, allowedProjectIds);

    Map<String, String> headers = new HashMap<>();
    headers.put(HttpHeaders.REFERER, "https://google.com");
    StepVerifier
      .create(controller.tunnel(envelope, headers))
      .expectNextMatches(responseEntity -> responseEntity.getStatusCode().is2xxSuccessful())
      .verifyComplete();

    client.verify(request, VerificationTimes.once());
  }

  @Test
  void testRetry() throws IOException {
    byte[] envelope = new ClassPathResource("/testbody.http").getInputStream().readAllBytes();

    HttpRequest requestMatcher = request()
      .withMethod(HttpMethod.POST.name())
      .withPath("/api/4/envelope/")
      .withBody(envelope);

    client
      .when(requestMatcher, Times.once())
      .respond(HttpResponse.response().withStatusCode(HttpStatus.GATEWAY_TIMEOUT.value()));

    client
      .when(requestMatcher, Times.once())
      .respond(HttpResponse.response().withStatusCode(HttpStatus.OK.value()));

    String path = "/telemetry";
    String url = "http://localhost:9000";
    HashSet<Integer> allowedProjectIds = Sets.newHashSet(1, 2, 4);
    SentryTunnelController controller = new SentryTunnelController(path, url, allowedProjectIds);

    Map<String, String> headers = new HashMap<>();
    StepVerifier
      .create(controller.tunnel(envelope, headers))
      .expectNextMatches(responseEntity -> responseEntity.getStatusCode().is2xxSuccessful())
      .verifyComplete();

    client.verify(requestMatcher, VerificationTimes.exactly(2));
  }

  @Test
  void testNoRetryOnClientErrors() throws IOException {
    byte[] envelope = new ClassPathResource("/testbody.http").getInputStream().readAllBytes();

    HttpRequest requestMatcher = request()
      .withMethod(HttpMethod.POST.name())
      .withPath("/api/4/envelope/")
      .withBody(envelope);

    client
      .when(requestMatcher, Times.once())
      .respond(HttpResponse.response().withStatusCode(HttpStatus.BAD_REQUEST.value()));

    String path = "/telemetry";
    String url = "http://localhost:9000";
    HashSet<Integer> allowedProjectIds = Sets.newHashSet(1, 2, 4);
    SentryTunnelController controller = new SentryTunnelController(path, url, allowedProjectIds);

    Map<String, String> headers = new HashMap<>();
    StepVerifier
      .create(controller.tunnel(envelope, headers))
      .expectErrorMessage("400 Bad Request from POST http://localhost:9000/api/4/envelope/")
      .verify();

    client.verify(requestMatcher, VerificationTimes.exactly(1));
  }
}
