using Dotnet.Sample.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mime;

namespace Dotnet.Sample.Infrastructure.Extensions
{
    public static class UseApiExceptionHandlerExtension
    {
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
}