using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xero.Demo.Api.Datastore;
using Xero.Demo.Api.Domain.Infrastructure;
using Xero.Demo.Api.Domain.Infrastructure.Extensions;
using Xero.Demo.Api.Domain.Models;

using Xero.Demo.Api.Xero.Demo.Domain.Models;

namespace Xero.Demo.Api.Domain.Extension
{
    internal static class AddMiddleWareExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddRolesAndPolicyAuthorization();
            services.AddJwtAuthentication(Configuration);
            services.AddDbContext<Database>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString(CONSTANTS.SqlLite))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                //.EnableSensitiveDataLogging()
                //.EnableDetailedErrors()
                //.LogTo(Console.WriteLine)
                ;
            });
            services.AddCors();
            services.AddLocalizationServices();

            services.AddApiVersioning(options =>
                {
                    options.ReportApiVersions = true;
                });

            services.AddVersionedApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(c =>
                {
                    c.OperationFilter<SwaggerDefaultValues>();
                    var securityDefinition = new OpenApiSecurityScheme()
                    {
                        Name = "Bearer",
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Description = "Specify the authorization token.",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                    }; ;
                    c.AddSecurityDefinition("jwt_auth", securityDefinition);

                    // Make sure swagger UI requires a Bearer token specified
                    OpenApiSecurityScheme securityScheme = new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "jwt_auth",
                            Type = ReferenceType.SecurityScheme
                        }
                    };
                    OpenApiSecurityRequirement securityRequirements = new OpenApiSecurityRequirement()
{
    {securityScheme, new string[] { }},
};
                    c.AddSecurityRequirement(securityRequirements);
                });

            services.AddFeatureManagement();
        }
    }
}