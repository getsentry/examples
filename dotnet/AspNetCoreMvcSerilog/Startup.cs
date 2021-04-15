using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sentry.Extensibility;

namespace AspNetCoreSerilog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Register as many ISentryEventExceptionProcessor as you need. They ALL get called.
            services.AddSingleton<ISentryEventExceptionProcessor, SpecialExceptionProcessor>();

            // You can also register as many ISentryEventProcessor as you need.
            services.AddTransient<ISentryEventProcessor, ExampleEventProcessor>();

            // To demonstrate taking a request-aware service into the event processor above
            services.AddHttpContextAccessor();

            services.AddSingleton<IGameService, GameService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = _ => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // The following middleware is added by default to ASP.NET Projects and displays the error page to the user
            // Exceptions handled by it are also captured by Sentry
            app.UseExceptionHandler("/Home/Error");

            app.UseHsts();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // This will add Sentry performance data (spans)
            app.UseSentryTracing();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
