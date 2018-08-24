using Sentry;
using Sentry.Extensibility;

namespace AspNetCore21Serilog
{
    public class SpecialExceptionProcessor : SentryEventExceptionProcessor<SpecialException>
    {
        protected override void ProcessException(
            SpecialException exception,
            SentryEvent sentryEvent)
        {
            sentryEvent.AddBreadcrumb("Processor running on special exception.");

            sentryEvent.SetTag("IsSpecial", exception.IsSpecial.ToString());
        }
    }
}
