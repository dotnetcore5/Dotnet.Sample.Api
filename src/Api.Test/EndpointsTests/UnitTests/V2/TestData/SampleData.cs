using Xero.Demo.Api.Domain.Models;
using System.Collections.Generic;

namespace Xero.Demo.Api.Tests.EndpointTests.UnitTests.V2.TestData
{
    internal class SampleDataV2
    {
        public static string productEndpoint = "/api/v{0}/products", DatabaseString = "Filename=Product.db", TraceIdentifier = "TraceIdentifier", Database = "Database", NewDescription = "NewDescription";

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

        public static IReadOnlyList<Product> Products
        {
            get
            {
                return new List<Product> { Product };
            }
        }

        public static Dictionary<string, string> Traits
        {
            get
            {
                return new Dictionary<string, string> { { "Test1", "Unit" }, { "Test2", "Integration" } };
            }
        }
    }
}