using Dotnet.Sample.Domain.Models;
using System;
using System.Collections.Generic;

namespace Dotnet.Sample.Api.Tests.EndpointTests.UnitTests.V1.TestData
{
    internal static class SampleDataV1
    {
        public const string productEndpoint = "/api/{0}/v{1}/products", DatabaseString = "Filename=Product.db", TraceIdentifier = "TraceIdentifier", Database = "Database", NewDescription = "NewDescription";
        public static Guid ProductId = Guid.NewGuid();
        public static string readerLoginEndpoint = "/api/{0}/v{1}/login/{2}";

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
    }
}