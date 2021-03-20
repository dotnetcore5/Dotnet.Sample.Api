using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Dotnet.Sample.Infrastructure.Extensions;
using Dotnet.Sample.Infrastructure.Languages;
using Microsoft.Extensions.Localization;

namespace Dotnet.Sample.Infrastructure
{
    public class Startup
    {
        public static IStringLocalizer Localizer; // This is how we access language strings

        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // this is where we store apps configuration including language
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddServices(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory logger, IApiVersionDescriptionProvider provider, EFStringLocalizerFactory localizerFactory)
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseApiExceptionHandler(logger);
            }
            app.UseMiddleware(provider, localizerFactory);
        }
    }
}