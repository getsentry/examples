package io.sentry.example;

import java.util.logging.Level;
import java.util.logging.Logger;


public class Application
{
    private static final Logger logger = Logger.getLogger("example.Application");

    public static void main(String[] args)
    {
        logger.fine("Debug message");
        logger.info("Info message");
        logger.warning("Warn message");

        try {
            int example = 1 / 0;
        } catch (Exception e) {
            logger.log(Level.SEVERE, "Caught exception!", e);
        }
    }
}
