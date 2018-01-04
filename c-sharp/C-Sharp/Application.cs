using System;
using SharpRaven;
using SharpRaven.Data;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var ravenClient = new RavenClient("https://<SENTRY_PUBLIC_KEY>:<SENTRY_PRIVATE_KEY>@sentry.io/<PROJECT_ID>");
            try
            {
                int i2 = 0;
                int i = 10 / i2;
            }
            catch (Exception exception)
            {
                ravenClient.Capture(new SentryEvent(exception));
            }
            Console.WriteLine("Hello World!");
        }
    }
}
