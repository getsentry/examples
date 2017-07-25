package io.sentry.example;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Application
{
    private static final Logger logger = LoggerFactory.getLogger("example.Application");

    public static void main(String[] args)
    {
        logger.debug("Debug message");
        logger.info("Info message");
        logger.warn("Warn message");

        try {
            int example = 1 / 0;
        } catch (Exception e) {
            logger.error("Caught exception!", e);
        }
    }
}
