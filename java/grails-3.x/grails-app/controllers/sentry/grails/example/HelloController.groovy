package sentry.grails.example

import org.slf4j.Logger
import org.slf4j.LoggerFactory

class HelloController {

    Logger log = LoggerFactory.getLogger(getClass())

    def index() {
        log.debug("Debug message")
        log.info("Info message")
        log.warn("Warn message")

        try {
            1 / 0
        } catch (Exception e) {
            log.error("Caught exception!", e)
        }

        render "hello, world!"
    }
}
