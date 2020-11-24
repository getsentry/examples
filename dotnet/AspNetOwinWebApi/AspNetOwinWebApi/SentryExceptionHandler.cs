using Sentry;
using System.Runtime.ExceptionServices;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Sentry.Protocol;

namespace AspNetOwinWebApi
{
    public class SentryExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            ExceptionDispatchInfo info = ExceptionDispatchInfo.Capture(context.Exception);

            // Add any contextual data you want to the event:
            var @event = new SentryEvent(info.SourceException)
            {
                Request = new Request
                {
                    Method = context.Request.Method.ToString(),
                    Url = context.Request.RequestUri.AbsoluteUri,
                    QueryString = context.Request.Headers.ToString()
                }
            };

            @event.SetTag("http-version", context.Request.Version.ToString());
            SentrySdk.CaptureEvent(@event);

            // Set the result:
            context.Result = new InternalServerErrorResult(context.Request);

            // Or re-throw if you want it to bubble up the middleware chain (Sentry removes duplicate captures)
            //info.Throw();
        }
    }
}