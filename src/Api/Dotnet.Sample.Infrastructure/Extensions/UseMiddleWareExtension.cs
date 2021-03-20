using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Collections.Generic;
using System.Globalization;
using Dotnet.Sample.Shared;
using Dotnet.Sample.Infrastructure.Languages;
using Dotnet.Sample.Infrastructure.Middleware;

namespace Dotnet.Sample.Infrastructure.Extensions
{
    internal static class UseMiddleWareExtension
    {
        public static void UseMiddleware(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, EFStringLocalizerFactory localizerFactory)
        {
            Startup.Localizer = localizerFactory.Create(null);
            var supportedCultures = new List<CultureInfo> { new CultureInfo(CONSTANTS.Languages[0]), new CultureInfo(CONSTANTS.Languages[1]) };
            var requestLocalizationOptions = new RequestLocalizationOptions { SupportedCultures = supportedCultures, SupportedUICultures = supportedCultures };
            requestLocalizationOptions.RequestCultureProviders.Insert(0, new JsonRequestCultureProvider());

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader())
                .UseRouting().UseAuthentication().UseAuthorization().UseRequestResponseLogging().UseMiddleware<JwtMiddleware>().UseSwagger().UseSwaggerUI(options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint(string.Format(CONSTANTS.SwaggerDetails.Endpoints, description.GroupName), description.GroupName.ToUpperInvariant());
                    }
                }).UseRequestLocalization(requestLocalizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "api/{{culture::regex(^[a-z]{{2}}-[A-Za-z]{{4}}$)}}/{controller}/{id?}");
            });
        }
    }
}