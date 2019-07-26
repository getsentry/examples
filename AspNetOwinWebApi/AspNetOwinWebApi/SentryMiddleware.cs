using Microsoft.Owin;
using Owin;
using Sentry;
using Sentry.Protocol;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetOwinWebApi
{
    internal static class SentryMiddlewareAppBuilderExtensions
    {
        public static void UseOwinExceptionHandler(this IAppBuilder app)
        {
            app.Use<SentryMiddleware>();
        }
    }

    internal class SentryMiddleware
    {
        private readonly Func<IDictionary<string, object>, Task> _next;

        public SentryMiddleware(Func<IDictionary<string, object>, Task> next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            using (SentrySdk.PushScope())
            {
                SentrySdk.ConfigureScope(scope =>
                {
                    // Gets called if an event is sent out within this request.
                    // By a logger.LogError or FirstChanceException
                    scope.AddEventProcessor(@event =>
                    {
                        ApplyContext(@event, environment);
                        return @event;
                    });
                });
                try
                {
                    await _next(environment);
                }
                catch (Exception ex)
                {
                    // An exception thrown in the ExceptionHandler will end up here.
                    var evt = new SentryEvent(ex);
                    ApplyContext(evt, environment);
                    SentrySdk.CaptureEvent(evt);

                    throw;
                }
            }
        }

        private static void ApplyContext(SentryEvent @event, IDictionary<string, object> env)
        {
            var context = new OwinContext(env);
          
            // Add Request data to the event here:
            @event.Request.Url = context.Request.Uri.AbsoluteUri;
            @event.Request.QueryString = context.Request.Headers.ToString();
            @event.Request.QueryString = context.Request.QueryString.ToString();
        }
    }
}