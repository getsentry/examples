# Sentry Basic Example

This project can be compiled and run with [Maven][maven]:

    mvn compile exec:java

To report to an existing Sentry installation, set the `SENTRY_DSN` environment
variable:

    SENTRY_DSN=https://public:private@host:port/1 mvn exec:java

[maven]: http://maven.apache.org/
[sentry-java]: https://github.com/getsentry/sentry-java