﻿using Rest.Api.Domain.Infrastructure.Extensions;
using Rest.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using Rest.Api.Datastore;

namespace Rest.Api.Domain.Extension
{
    internal static class AddMiddleWareExtensions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers();

            services.AddDbContext<Database>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString(CONSTANTS.SqlLite))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                .LogTo(Console.WriteLine)
                ;
            });

            services.AddScoped<IDatabase>(provider => provider.GetService<Database>());

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

                    //var xmlFile = $"Rest.Api.xml";
                    //c.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile));
                });

            services.AddFeatureManagement();
        }
    }
}