using Dotnet.Sample.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Dotnet.Sample.Infrastructure.Extensions
{
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}