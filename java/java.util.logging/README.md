# Sentry java.util.logging Example

This project contains an example Sentry integration with `java.util.logging`.

When using `java.util.logging` the JVM must be started with all handler classes
available, so this project uses Maven assembly to build a JAR containing all
dependencies that may be run directly via `java`.

    # Build the JAR
    mvn clean package

    # Run the JAR
    SENTRY_DSN="https://public:private@host:port/1" \
    java \
    -Djava.util.logging.config.file=src/main/resources/logging.properties \
    -cp ./target/sentry-java-jul-example-1.0-SNAPSHOT-jar-with-dependencies.jar \
    io.sentry.example.Application
