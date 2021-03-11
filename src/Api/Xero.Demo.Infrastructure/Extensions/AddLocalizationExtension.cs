using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Xero.Demo.Api.Domain.Languages;

namespace Xero.Demo.Api.Domain.Infrastructure.Extensions
{
    public static class AddLocalizationExtension
    {
        public static IStringLocalizer _e; // This is how we access language strings
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