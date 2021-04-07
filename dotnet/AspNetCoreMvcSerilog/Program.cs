using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace AspNetCoreSerilog
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSerilog((_, c) =>
                        c.Enrich.FromLogContext()
                            .MinimumLevel.Debug()
                            .WriteTo.Console()
                            // Add Sentry integration with Serilog
                            // Two levels are used to configure it.
                            // One sets which log level is minimally required to keep a log message as breadcrumbs
                            // The other sets the minimum level for messages to be sent out as events to Sentry
                            .WriteTo.Sentry(s =>
                            {
                                s.MinimumBreadcrumbLevel = LogEventLevel.Debug;
                                s.MinimumEventLevel = LogEventLevel.Error;
                            }));

                    // Example integration with advanced configuration scenarios:
                    webBuilder.UseSentry(options =>
                    {
                        // The parameter 'options' here has values populated through the configuration system.
                        // That includes 'appsettings.json', environment variables and anything else
                        // defined on the ConfigurationBuilder.
                        // See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-2.1&tabs=basicconfiguration

                        // Tracks the release which sent the event and enables more features: https://docs.sentry.io/learn/releases/
                        // If not explicitly set here, the SDK attempts to read it from: AssemblyInformationalVersionAttribute and AssemblyVersion
                        // TeamCity: %build.vcs.number%, VSTS: BUILD_SOURCEVERSION, Travis-CI: TRAVIS_COMMIT, AppVeyor: APPVEYOR_REPO_COMMIT, CircleCI: CIRCLE_SHA1
                        options.Release =
                            "e386dfd"; // Could be also the be like: 2.0 or however your version your app

                        options.MaxBreadcrumbs = 200;

                        // Optionally, set an HTTP proxy (it's the same for HTTP and HTTPS connections)
                        options.HttpProxy = null; // new WebProxy("https://localhost:3128");

                        options.MaxQueueItems = 100;
                        options.ShutdownTimeout = TimeSpan.FromSeconds(5);

                        // NOTE: Hard-coding here will override any value set on appsettings.json:
                        options.MinimumEventLevel = LogLevel.Error;
                    });

                    webBuilder.UseSerilog();

                    webBuilder.UseStartup<Startup>();
                });
    }
}
