Sentry Grails Example
====================

This project contains an example Sentry integration with Grails. It uses
the default `logback` logging framework to send log level `WARN` and above
to sentry.

The example application can be started as follows (set `SENTRY_DSN` to a
proper value):

    SENTRY_DSN="https://public:private@host:port/1" grails run-app
    
Now, visit `http://localhost:8080/hello/index` in your browser and an
`ERROR` message should be sent to the Sentry server you point to in your 
`SENTRY_DSN`.

See
[grails-app/conf/logback.groovy](https://github.com/getsentry/examples/blob/master/java/grails-3.x/grails-app/conf/logback.groovy#L13-L18)
for example logger configuration and
[build.gradle](https://github.com/getsentry/examples/blob/master/java/grails-3.x/build.gradle#L44)
for the dependency on `io.sentry:sentry-logback`.
