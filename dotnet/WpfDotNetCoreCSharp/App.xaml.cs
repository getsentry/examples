using System.Windows;
using System.Windows.Threading;
using Sentry;

namespace WpfDotNetCoreCSharp
{
    public partial class App : Application
    {
        public App()
        {
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            // NOTE: Change the URL below to your own DSN. Get it on sentry.io in the project settings (or when you create a new project):
            SentrySdk.Init("https://80aed643f81249d4bed3e30687b310ab@o447951.ingest.sentry.io/5428537");
        }
        
        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            SentrySdk.CaptureException(e.Exception);

            // If you want to stop the application from crashing, uncomment this line:
            // e.Handled = true;
        }
    }
}
