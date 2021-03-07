using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Xero.Demo.Api.Domain.Security;

namespace Xero.Demo.Api.Domain.Infrastructure
{
    public static partial class AddAuthorizationExtension
    {
        public static IServiceCollection AddRolesAndPolicyAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(
                config =>
                {
                    config.AddPolicy("ShouldBeAnAdmin", options =>
                    {
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAnAdminRequirement());
                    });

                    config.AddPolicy("ShouldBeAnEditor", options =>
                    {
                        options.RequireClaim(ClaimTypes.Role);
                        options.RequireRole("Reader");
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAReaderRequirement());
                    });

                    config.AddPolicy("ShouldBeAReader", options =>
                    {
                        options.RequireClaim(ClaimTypes.Role);
                        options.RequireRole("Reader");
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAReaderRequirement());
                    });

                    config.AddPolicy("ShouldContainRole", options =>
                        options.RequireClaim(ClaimTypes.Role));
                });

            return services;
        }
    }
}