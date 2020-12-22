using System;
using System.Windows.Forms;
using Sentry;

namespace WindowsFormsCSharp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // NOTE: Change the URL below to your own DSN. Get it on sentry.io in the project settings (or when you create a new project):
            SentrySdk.Init("https://80aed643f81249d4bed3e30687b310ab@o447951.ingest.sentry.io/5428537");

            // Configure WinForms pass the exception to Sentry
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.ThrowException);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
