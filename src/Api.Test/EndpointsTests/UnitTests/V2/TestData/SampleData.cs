using Dotnet.Sample.Domain.Models;

namespace Dotnet.Sample.Api.Tests.EndpointTests.UnitTests.V2.TestData
{
    internal static class SampleDataV2
    {
        public const string productEndpoint = "/api/v{0}/products", DatabaseString = "Filename=Product.db", TraceIdentifier = "TraceIdentifier", Database = "Database", NewDescription = "NewDescription";

        public static Product Product
        {
            get
            {
                return new Product
                {
                    Name = "product1",
                    Description = "Description",
                    Price = 11,
                    DeliveryPrice = 12
                };
            }
        }
    }
}