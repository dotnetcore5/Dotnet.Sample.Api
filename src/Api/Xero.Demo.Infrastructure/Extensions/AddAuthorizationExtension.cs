using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using Xero.Demo.Api.Domain.Security;
using Xero.Demo.Api.Xero.Demo.Domain.Models;

namespace Xero.Demo.Api.Domain.Infrastructure
{
    public static partial class AddAuthorizationExtension
    {
        public static IServiceCollection AddRolesAndPolicyAuthorization(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
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

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.Audience = "http://localhost:5001/";
                options.Authority = "http://localhost:5000/";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Secret"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            return services;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                // not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}