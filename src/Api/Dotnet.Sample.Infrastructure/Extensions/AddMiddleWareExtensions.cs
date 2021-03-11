using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;
using Dotnet.Sample.Datastore;
using Dotnet.Sample.Domain.Models;
using Dotnet.Sample.Shared;
using Dotnet.Sample.Infrastructure.Swagger;

namespace Dotnet.Sample.Infrastructure.Extensions
{
    internal static class AddMiddleWareExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            services.AddSecurity(Configuration);

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
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                    c.OperationFilter<SwaggerDefaultValues>();
                    var securityDefinition = new OpenApiSecurityScheme
                    {
                        Name = "Bearer",
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                        Description = "Specify the authorization token.",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                    };
                    c.AddSecurityDefinition("jwt_auth", securityDefinition);
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Id = "jwt_auth",
                                    Type = ReferenceType.SecurityScheme
                                }
                            },
                            new string[] { }},
                    });
                });

            services.AddFeatureManagement();
        }
    }
}