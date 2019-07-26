Integrates an OWIN app with Sentry.

Two test endpoints:

http://localhost:50612/api/default

This endpoint throws an exception that is handled by the ExceptionHandler


And
http://localhost:50612/api/handled

This endpoint has a try/catch and handles the exception without logging. The exception is captured with Sentry by using AppDomain.FirstChanceException
The request data is added to the event through the SentryEventProcessor defined in the SentryMiddleware.
A new Scope is pushed to isolate request data from each other.


**Make sure to add your own DSN before running so you can capture the errors in your Sentry project**

![Sample event in Sentry](SampleEvent.JPG)
