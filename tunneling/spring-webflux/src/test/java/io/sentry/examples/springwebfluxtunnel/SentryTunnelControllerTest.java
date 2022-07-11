package io.sentry.examples.springwebfluxtunnel;

import com.google.common.collect.Sets;

import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.mockserver.integration.ClientAndServer;
import org.mockserver.junit.jupiter.MockServerExtension;
import org.mockserver.junit.jupiter.MockServerSettings;
import org.mockserver.model.HttpResponse;
import org.springframework.core.io.ClassPathResource;
import org.springframework.http.HttpMethod;
import org.springframework.http.HttpStatus;

import java.io.IOException;
import java.util.HashSet;

import reactor.test.StepVerifier;

import static org.mockserver.model.HttpRequest.request;

@ExtendWith(MockServerExtension.class)
@MockServerSettings(ports = { 9000 })
class SentryTunnelControllerTest {

  private final ClientAndServer client;

  public SentryTunnelControllerTest(ClientAndServer client) {
    this.client = client;
  }

  @Test
  void testForwarding() throws IOException {
    byte[] envelope = new ClassPathResource("/testbody.http").getInputStream().readAllBytes();

    client
      .when(
        request().withMethod(HttpMethod.POST.name()).withPath("/api/4/envelope/").withBody(envelope)
      )
      .respond(HttpResponse.response().withStatusCode(HttpStatus.OK.value()));

    String path = "/telemetry";
    String url = "http://localhost:9000";
    HashSet<Integer> allowedProjectIds = Sets.newHashSet(1, 2, 4);
    SentryTunnelController controller = new SentryTunnelController(path, url, allowedProjectIds);

    StepVerifier.create(controller.tunnel(envelope)).expectNextCount(1).verifyComplete();
  }
}
