# Sentry Spring Boot Example

This project contains an example Sentry integration with Spring Boot. 

The example application can be started as follows (set `SENTRY_DSN` to a
proper value):

    SENTRY_DSN="https://public:private@host:port/1" mvn spring-boot:run
    
Now, visit `http://localhost:8080/` in uncaught exceptions should be sent to the Sentry server you point to in your 
`SENTRY_DSN`.
