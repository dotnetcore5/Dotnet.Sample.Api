using Dotnet.Sample.Shared;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Dotnet.Sample.Infrastructure.Swagger
{
    internal class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = CONSTANTS.SwaggerDetails.Title,
                Contact = new OpenApiContact { Name = CONSTANTS.SwaggerDetails.ContactName, Email = CONSTANTS.SwaggerDetails.ContactEmail, Url = new Uri(CONSTANTS.SwaggerDetails.ContactUrl) },
                Description = string.Format(CONSTANTS.SwaggerDetails.ApiDescription, description.ApiVersion.ToString()),
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += CONSTANTS.SwaggerDetails.ApiVersionDescription;
            }

            return info;
        }
    }
}