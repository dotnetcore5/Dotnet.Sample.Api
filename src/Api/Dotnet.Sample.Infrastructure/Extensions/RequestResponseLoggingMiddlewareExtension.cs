using Dotnet.Sample.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Dotnet.Sample.Infrastructure.Extensions
{
    internal static class RequestResponseLoggingMiddlewareExtension
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}