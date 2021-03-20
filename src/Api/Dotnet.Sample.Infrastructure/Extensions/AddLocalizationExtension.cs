using Dotnet.Sample.Infrastructure.Languages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Dotnet.Sample.Infrastructure.Extensions
{
    internal static class AddLocalizationExtension
    {
        public static IServiceCollection AddLocalizationServices(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizerFactory, EFStringLocalizerFactory>();
            //services.AddSingleton<IStringLocalizer, EFStringLocalizer>();

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            });

            return services;
        }
    }
}