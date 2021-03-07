using Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.TestData;
using Xero.Demo.Api.Tests.Setup;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;
using Xero.Demo.Api.Domain;
using Xero.Demo.Api.Domain.Models;

namespace Xero.Demo.Api.Tests.EndpointTests.IntegrationTests
{
    [Trait("Category", "Integration")]
    public class ProductControllerTest : IDisposable
    {
        private CustomWebApplicationFactory<Startup> factory;

        public ProductControllerTest()
        {
            factory = new CustomWebApplicationFactory<Startup>();
        }

        [Theory]
        [InlineData("en-US", "1")]
        //[InlineData("en-US", "2")]
        public async Task GetAsync_Returns_200(string culture, string version)
        {
            // Given
            var client = factory.CreateClient();

            // When
            var response = await client.GetAsync(string.Format(SampleDataV1.productEndpoint, culture, version));

            // Then
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("en-US", "1")]
        [InlineData("en-US", "2")]
        public async Task GetByIdAsync_Returns_200(string culture, string version)
        {
            // Given
            var client = factory.CreateClient();
            var addProductResponse = await client.PostAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version), SampleDataV1.Product);
            var addedProduct = JsonConvert.DeserializeObject<Product>(await addProductResponse.Content.ReadAsStringAsync());
            var id = addedProduct.Id;

            // When
            var response = await client.GetAsync(string.Format(SampleDataV1.productEndpoint, culture, version) + $"/{id}");

            // Then
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("en-US", "1")]
        [InlineData("en-US", "2")]
        public async Task PostAsync_Returns_201(string culture, string version)
        {
            // Given
            var client = factory.CreateClient();

            // When
            var response = await client.PostAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version), SampleDataV1.Product);

            // Then
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("en-US", "1")]
        [InlineData("en-US", "2")]
        public async Task PutAsync_Returns_204(string culture, string version)
        {
            // Given
            var client = factory.CreateClient();
            var addResponse = await client.PostAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version), SampleDataV1.Product);

            var productResponse = await client.GetAsync(string.Format(SampleDataV1.productEndpoint, culture, version));
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(await productResponse.Content.ReadAsStringAsync());
            var id = products.FirstOrDefault().Id;
            var putRequestPayload = new Product
            {
                Id = products.FirstOrDefault().Id,
                Name = products.FirstOrDefault().Name,
                DeliveryPrice = products.FirstOrDefault().DeliveryPrice,
                Price = products.FirstOrDefault().Price,
                Description = products.FirstOrDefault().Description
            };

            // When
            var response = await client.PutAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version) + $"/{id}", putRequestPayload);

            // Then
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        public void Dispose()
        {
            factory = null;
        }
    }
}