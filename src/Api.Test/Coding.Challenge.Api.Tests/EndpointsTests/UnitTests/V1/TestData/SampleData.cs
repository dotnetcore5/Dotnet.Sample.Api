using Rest.Api.Domain.Models;
using System;
using System.Collections.Generic;

namespace Rest.Api.Tests.EndpointTests.UnitTests.V1.TestData
{
    internal class SampleDataV1
    {
        public static string productEndpoint = "/api/{0}/v{1}/products", DatabaseString = "Filename=Product.db", TraceIdentifier = "TraceIdentifier", Database = "Database", NewDescription = "NewDescription";
        public static Guid ProductId = Guid.NewGuid();

        public static Product Product
        {
            get
            {
                return new Product
                {
                    //Id = ProductId,
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