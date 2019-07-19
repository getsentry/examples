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
            try
            {
                await _next(environment);
            }
            catch (Exception ex)
            {
                // An exception thrown in the ExceptionHandler will end up here.
                var owinContext = new OwinContext(environment);

                SentrySdk.CaptureEvent(new SentryEvent(ex)
                {
                    Request = new Request
                    {
                        QueryString = owinContext.Request.QueryString.ToString()
                    }
                });

                throw;
            }
        }
    }
}