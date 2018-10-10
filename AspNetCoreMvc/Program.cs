using System;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Sentry.Samples.AspNetCore.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()

                // Example integration with advanced configuration scenarios:
                .UseSentry(o =>
                {
                    // The parameter 'o' here has values populated through the configuration system.
                    // That includes 'appsettings.json', environment variables and anything else defined on the ConfigurationBuilder.
                    // See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration

                    // Tracks the release which sent the event and enables more features: https://docs.sentry.io/learn/releases/
                    // If not explicitly set here, the SDK attempts to read it from: AssemblyInformationalVersionAttribute and AssemblyVersion
                    // TeamCity: %build.vcs.number%, VSTS: BUILD_SOURCEVERSION, Travis-CI: TRAVIS_COMMIT, AppVeyor: APPVEYOR_REPO_COMMIT, CircleCI: CIRCLE_SHA1
                    // o.Release = "b2946b7e477e1b63dd24db3db4bfdd602e302eec"; // Could be also the be like: 2.0 or however your version your app

                    o.MaxBreadcrumbs = 200;

                    //o.HttpProxy = new WebProxy("https://localhost:3128");

                    // Example: Disabling support to compressed responses:
                    o.DecompressionMethods = DecompressionMethods.None;

                    o.MaxQueueItems = 100;
                    o.ShutdownTimeout = TimeSpan.FromSeconds(5);

                    // Hard-coding here will override any value set on appsettings.json:
                    o.MinimumEventLevel = LogLevel.Error;
                })
                .Build();
    }
}
