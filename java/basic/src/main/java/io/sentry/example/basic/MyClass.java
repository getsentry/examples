package io.sentry.example.basic;

import io.sentry.Sentry;
import io.sentry.SentryClient;
import io.sentry.protocol.User;

public class MyClass {
    private static SentryClient sentry;

    public static void main(String... args) {
        /*
        Sentry can read the DSN from the environment variable "SENTRY_DSN", the Java
        System Property "sentry.dsn", or the "sentry.properties" file
        in your classpath. This makes it easier to provide and adjust
        your DSN without needing to change your code. See the configuration
        page for more information.
        */
        Sentry.init();

        // You can also manually provide the DSN to the ``init`` method.
        // String dsn = "https://<SENTRY_PUBLIC_KEY>:<SENTRY_PRIVATE_KEY>@sentry.io/<PROJECT_ID>";
        // Sentry.init(options -> {
        //     options.setDsn(dsn);
        // });

        MyClass myClass = new MyClass();
        myClass.logWithStaticAPI();
    }

    /**
     * An example method that throws an exception.
     */
    void unsafeMethod() {
        throw new UnsupportedOperationException("You shouldn't call this!");
    }

    /**
     * Examples using the (recommended) static API.
     */
    void logWithStaticAPI() {
        // Note that all fields set on the context are optional. Context data is copied onto
        // all future events in the current context (until the context is cleared).

        // Record a breadcrumb in the current context. By default the last 100 breadcrumbs are kept.
        Sentry.addBreadcrumb("User made an action");

        // Set the user in the current context.
        User user = new User();
        user.setEmail("hello@sentry.io");

        // Add extra data to future events in this context.
        Sentry.setExtra("extra", "thing");

        // Add an additional tag to future events in this context.
        Sentry.setTag("tagName", "tagValue");

        /*
        This sends a simple event to Sentry using the statically stored instance
        that was created in the ``main`` method.
        */
        Sentry.captureMessage("This is a test");

        try {
            unsafeMethod();
        } catch (Exception e) {
            // This sends an exception event to Sentry using the statically stored instance
            // that was created in the ``main`` method.
            Sentry.captureException(e);
        }
    }

}
