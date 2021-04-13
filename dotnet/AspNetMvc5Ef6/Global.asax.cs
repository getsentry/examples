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

            // Start a transaction that wraps the current request and bind it to the scope
            var transaction = SentrySdk.StartTransaction(
                $"{method} {path}",
                "http.server"
            );

            SentrySdk.ConfigureScope(scope => scope.Transaction = transaction);
        }

        protected void Application_EndRequest()
        {
            // Finish the currently active span
            SentrySdk.GetSpan()?.Finish();
        }
    }
}
