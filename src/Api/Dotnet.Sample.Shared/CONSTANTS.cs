namespace Dotnet.Sample.Shared
{
    public class CONSTANTS
    {
        public const string SqlLite = "SqlLite";

        public static string[] Languages = { "en-US", "fr-FR" };

        public const string LanguageLocation = "Dotnet.Sample.Infrastructure/Languages/Resources/{0}.json";
        public const string Authorization = "Authorization";

        public static class RouteNames
        {
            public const string GetAsync = "GetAsync";
            public const string GetByIdAsync = "GetByIdAsync";
            public const string PostAsync = "PostAsync";
            public const string PutAsync = "PutAsync";
            public const string DeleteAsync = "DeleteAsync";
            public const string RouteAttribute = "api/{culture:culture}/v{version:apiVersion}/[controller]";
        }

        public class CustomException
        {
            public const string GlobalException = "GlobalException";
            public const string LogExceptionMessage = "Something went wrong: {0}";
            public const string ShowExceptionMessage = "Something went wrongs.Please try again later";
            public const string NotFoundException = "Product not found. id = {0}";
        }

        public class Features
        {
            public const string PRODUCT = "PRODUCT";
            public const string PRODUCTOPTION = "PRODUCTOPTION";
        }

        public class ApiVersionNumbers
        {
            public const string V1 = "1.0";
            public const string V2 = "2.0";
        }

        public class ApiAnnotations
        {
            public const string Produces = "application/json";
            public const string Consumes = "application/json";
        }

        public class SwaggerDetails
        {
            public const string Endpoints = "/swagger/{0}/swagger.json";
            public const string Title = "Dotnet.Sample.Api";
            public const string ApiDescription = " REST API v{0} has been implemented.";
            public const string ApiVersionDescription = "Now, this API version [v{0}] has been deprecated.";
            public const string ContactName = "ajeet";
            public const string ContactEmail = "ajeetx@email.com";
            public const string ContactUrl = "https://ajeetx.github.io/";
        }

        public class Roles
        {
            public const string Admin = "Admin";
            public const string Editor = "Editor";
            public const string Reader = "Reader";
        }

        public class Policy
        {
            public const string ShouldBeAnAdmin = "ShouldBeAnAdmin";
            public const string ShouldBeAnEditor = "ShouldBeAnEditor";
            public const string ShouldBeAReader = "ShouldBeAReader";
        }
    }
}