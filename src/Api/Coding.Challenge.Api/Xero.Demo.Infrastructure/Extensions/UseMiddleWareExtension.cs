using Xero.Demo.Api.Domain.Infrastructure.Extensions;
using Xero.Demo.Api.Domain.Languages;
using Xero.Demo.Api.Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Xero.Demo.Api.Domain.Extension
{
    internal static class UseMiddleWareExtension
    {
        public static void UseMiddleware(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, EFStringLocalizerFactory localizerFactory)
        {
            app.UseHttpsRedirection().UseRouting().UseAuthorization().UseSwagger().UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint(string.Format(CONSTANTS.SwaggerDetails.Endpoints, description.GroupName), description.GroupName.ToUpperInvariant());
                }
            });

            AddLocalizationExtension._e = localizerFactory.Create(null);

            var supportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("fr-FR") };

            var requestLocalizationOptions = new RequestLocalizationOptions { SupportedCultures = supportedCultures, SupportedUICultures = supportedCultures };
            requestLocalizationOptions.RequestCultureProviders.Insert(0, new JsonRequestCultureProvider());
            app.UseRequestLocalization(requestLocalizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                            name: "default",
                            pattern: "api/{{culture::regex(^[a-z]{{2}}-[A-Za-z]{{4}}$)}}/{controller}/{id?}");
            });
        }

        public static void UseApiExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)                                                                                     //if any exception then report it and log it
                    {
                        var logger = loggerFactory.CreateLogger(CONSTANTS.CustomException.GlobalException);                         //Technical Exception for troubleshooting
                        logger.LogError(string.Format(CONSTANTS.CustomException.LogExceptionMessage, contextFeature.Error));

                        //Business exception - exit gracefully
                        await context.Response.WriteAsync(new
                        {
                            context.Response.StatusCode,
                            Message = CONSTANTS.CustomException.ShowExceptionMessage
                        }.ToString());
                    }
                });
            });
        }
    }

    public class JsonRequestCultureProvider : RequestCultureProvider
    {
        private static readonly Regex LocalePattern = new Regex(@"^[a-z]{2}(-[a-z]{2,4})?$", RegexOptions.IgnoreCase);

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

            var parts = httpContext.Request.Path.Value.Split('/');
            if (parts.Length < 3) return Task.FromResult<ProviderCultureResult>(null);

            if (!LocalePattern.IsMatch(parts[2])) return Task.FromResult<ProviderCultureResult>(null);

            var culture = parts[2];

            culture = culture ?? "en-US";

            return Task.FromResult(new ProviderCultureResult(culture));
        }
    }
}