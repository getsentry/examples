using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin.BuilderProperties;
using Owin;
using Sentry;

namespace AspNetOwinWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // ADD YOUR DSN BELOW:
            var flush = SentrySdk.Init("https://5fd7a6cda8444965bade9ccfd3df9882@sentry.io/1188141");
            var properties = new AppProperties(app.Properties);
            properties.OnAppDisposing.Register(() => flush.Dispose());

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Services.Replace(typeof(IExceptionHandler), new SentryExceptionHandler());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseOwinExceptionHandler();
            app.UseWebApi(config);
        }
    }
}