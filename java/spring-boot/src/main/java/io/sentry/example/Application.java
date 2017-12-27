package io.sentry.example;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.EnableAutoConfiguration;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.boot.web.servlet.ServletContextInitializer;
import org.springframework.context.annotation.Bean;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.servlet.HandlerExceptionResolver;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.slf4j.MDC;

@Controller
@EnableAutoConfiguration
@SpringBootApplication
public class Application {

    private static final Logger logger = LoggerFactory.getLogger("example.Application");

    /*
    Register a HandlerExceptionResolver that sends all exceptions to Sentry
    and then defers all handling to the other HandlerExceptionResolvers.

    You should only register this is you are not using a logging integration,
    otherwise you may double report exceptions.
     */
    @Bean
    public HandlerExceptionResolver sentryExceptionResolver() {
        return new io.sentry.spring.SentryExceptionResolver();
    }

    /*
    Register a ServletContextInitializer that installs the SentryServletRequestListener
    so that Sentry events contain HTTP request information.

    This should only be necessary in Spring Boot applications. "Classic" Spring
    should automatically load the `io.sentry.servlet.SentryServletContainerInitializer`.
     */
    @Bean
    public ServletContextInitializer sentryServletContextInitializer() {
        return new io.sentry.spring.SentryServletContextInitializer();
    }

    @RequestMapping("/")
    @ResponseBody
    String home() {
        MDC.put("customKey", "customValue");

        logger.debug("Debug message");
        logger.info("Info message");
        logger.warn("Warn message");

        try {
            int example = 1 / 0;
        } catch (Exception e) {
            logger.error("Caught exception!", e);
        }

        int x = 1 / 0; // uncaught exception!

        return "Hello World";
    }

    public static void main(String[] args) {
        SpringApplication.run(Application.class, args);
    }
}
