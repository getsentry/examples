# Sentry Scala + Logback Example

This project contains an integration example written in Scala and uses Sentry's Logback SDK.


## Installation and Configuration
Add the Sentry Logback package as dependency to the [build.sbt](https://github.com/getsentry/examples/blob/master/scala/logback/build.sbt#L8) file.

View full documentation of Sentry's Logback integration [here](https://docs.sentry.io/clients/java/modules/logback/#installation). 


## Run the Example
To report to an existing Sentry installation, set the `SENTRY_DSN` environment
variable:

    SENTRY_DSN=https://public:private@host:port/1 sbt run
