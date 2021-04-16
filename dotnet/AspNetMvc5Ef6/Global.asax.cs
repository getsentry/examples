using System;
using Sentry;
using Sentry.EntityFramework;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using Sentry.AspNet;

namespace AspNetMvc5Ef6
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IDisposable _sentry;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // We add the query logging here so multiple DbContexts in the same project are supported
            SentryDatabaseLogging.UseBreadcrumbs();

            // Set up the sentry SDK
            _sentry = SentrySdk.Init(o =>
            {
                // We store the DSN inside Web.config; make sure to use your own DSN!
                o.Dsn = ConfigurationManager.AppSettings["SentryDsn"];

                // Get ASP.NET integration
                o.AddAspNet();

                // Get Entity Framework integration
                o.AddEntityFramework();
            });
        }

        // Global error catcher
        protected void Application_Error()
        {
            var exception = Server.GetLastError();

            // Capture unhandled exceptions
            SentrySdk.CaptureException(exception);
        }

        protected void Application_End()
        {
            // Close the Sentry SDK (flushes queued events to Sentry)
            _sentry?.Dispose();
        }

        protected void Application_BeginRequest()
        {
            var method = Context.Request.HttpMethod;
            var path = Context.Request.Path;

            // Start a transaction that encompasses the current request
            var transaction = SentrySdk.StartTransaction(
                $"{method} {path}",
                "http.server"
            );

            // Attach the transaction to the scope so that other operations can reference it
            SentrySdk.ConfigureScope(scope => scope.Transaction = transaction);

            // Attach transaction to the request context to finish it when the request ends
            Context.Items["__SentryTransaction"] = transaction;
        }

        protected void Application_EndRequest()
        {
            // Finish the currently active transaction
            if (Context.Items.Contains("__SentryTransaction"))
            {
                var transaction = Context.Items["__SentryTransaction"] as ISpan;

                var status =
                    Context.Response.StatusCode < 400 ? SpanStatus.Ok :
                    Context.Response.StatusCode == 400 ? SpanStatus.InvalidArgument :
                    Context.Response.StatusCode == 401 ? SpanStatus.Unauthenticated :
                    Context.Response.StatusCode == 403 ? SpanStatus.PermissionDenied :
                    Context.Response.StatusCode == 404 ? SpanStatus.NotFound :
                    Context.Response.StatusCode == 409 ? SpanStatus.AlreadyExists :
                    Context.Response.StatusCode == 429 ? SpanStatus.ResourceExhausted :
                    Context.Response.StatusCode == 499 ? SpanStatus.Cancelled :
                    Context.Response.StatusCode < 500 ? SpanStatus.InvalidArgument :
                    Context.Response.StatusCode == 500 ? SpanStatus.InternalError :
                    Context.Response.StatusCode == 501 ? SpanStatus.Unimplemented :
                    Context.Response.StatusCode == 503 ? SpanStatus.Unavailable :
                    Context.Response.StatusCode == 504 ? SpanStatus.DeadlineExceeded :
                    Context.Response.StatusCode < 600 ? SpanStatus.InternalError :
                    SpanStatus.UnknownError;

                transaction?.Finish(status);
            }
        }
    }
}
