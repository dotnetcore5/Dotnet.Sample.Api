using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using Dotnet.Sample.Domain.Services;
using Dotnet.Sample.Shared;
using Dotnet.Sample.Infrastructure.Security;

namespace Dotnet.Sample.Infrastructure.Extensions
{
    public static partial class AddSecurityExtension
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.RequireHttpsMetadata = false;
                            options.SaveToken = true;
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuer = true,
                                ValidateAudience = true,
                                ValidateLifetime = true,
                                ValidateIssuerSigningKey = true,
                                ValidIssuer = configuration["JwtSettings:Issuer"],
                                ValidAudience = configuration["JwtSettings:Audience"],
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
                                ClockSkew = TimeSpan.Zero
                            };
                        });

            services.AddAuthorization(
                        config =>
                {
                    config.AddPolicy(CONSTANTS.Policy.ShouldBeAnAdmin, options =>
                    {
                        options.RequireClaim(ClaimTypes.Role);
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAnAdminRequirement());
                    });

                    config.AddPolicy(CONSTANTS.Policy.ShouldBeAnEditor, options =>
                    {
                        options.RequireClaim(ClaimTypes.Role);
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAnEditorRequirement());
                    });

                    config.AddPolicy(CONSTANTS.Policy.ShouldBeAReader, options =>
                    {
                        options.RequireClaim(ClaimTypes.Role);
                        options.RequireAuthenticatedUser();
                        options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                        options.Requirements.Add(new ShouldBeAReaderRequirement());
                    });
                });

            services.AddScoped<IAuthorizationHandler, ShouldBeAnAdminRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, ShouldBeAReaderAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, ShouldBeAnEditorRequirementHandler>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}