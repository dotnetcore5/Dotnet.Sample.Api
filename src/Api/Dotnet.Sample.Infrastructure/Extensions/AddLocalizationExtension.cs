using Dotnet.Sample.Infrastructure.Languages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Dotnet.Sample.Infrastructure.Extensions
{
    public static class AddLocalizationExtension
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static IServiceCollection AddLocalizationServices(this IServiceCollection services)
        {
            services.AddTransient<EFStringLocalizerFactory>();

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            });

            return services;
        }
    }
}