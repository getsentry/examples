using Sentry;
using Sentry.EntityFramework;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AspNetMvc5Ef6
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // We add the query logging here so multiple DbContexts in the same project are supported
            SentryDatabaseLogging.UseBreadcrumbs();

            // Set up the sentry SDK
            SentrySdk.Init(o =>
            {
                // We store the DSN inside Web.config; make sure to use your own DSN!
                o.Dsn = new Dsn(ConfigurationManager.AppSettings["SentryDsn"]);
                o.AddEntityFramework();
            });
        }

        // Global error catcher
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            SentrySdk.CaptureException(exception);
        }
    }
}
