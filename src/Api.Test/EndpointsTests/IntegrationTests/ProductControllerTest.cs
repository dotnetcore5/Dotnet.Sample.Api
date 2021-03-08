using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xero.Demo.Api.Domain;
using Xero.Demo.Api.Domain.Models;
using Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.TestData;
using Xero.Demo.Api.Tests.Setup;
using Xero.Demo.Api.Xero.Demo.Domain.Models;
using Xunit;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;

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
        public async Task GetAsync_Returns_200(string culture, string version)
        {
            // Given
            var client = factory.CreateClient();
            var authResponse = await client.PostAsync(string.Format(SampleDataV1.readerLoginEndpoint, culture, version, Roles.Reader), null);
            var authDetails = JsonConvert.DeserializeObject<AuthenticateResponse>(await authResponse.Content.ReadAsStringAsync());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authDetails.Token);

            // When
            var response = await client.GetAsync(string.Format(SampleDataV1.productEndpoint, culture, version));

            // Then
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("en-US", "1")]
        public async Task GetByIdAsync_Returns_200(string culture, string version)
        {
            // Given
            var client = factory.CreateClient();
            var authResponse = await client.PostAsync(string.Format(SampleDataV1.readerLoginEndpoint, culture, version, Roles.Reader), null);
            var authDetails = JsonConvert.DeserializeObject<AuthenticateResponse>(await authResponse.Content.ReadAsStringAsync());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authDetails.Token);
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
        public async Task PostAsync_Returns_201(string culture, string version)
        {
            // Given
            var client = await SetupHttpClient(Roles.Admin, culture, version);

            // When
            var response = await client.PostAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version), SampleDataV1.Product);

            // Then
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("en-US", "1")]
        public async Task PutAsync_Returns_204(string culture, string version)
        {
            // Given
            var client = await SetupHttpClient(Roles.Admin, culture, version);
            await client.PostAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version), SampleDataV1.Product);

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

            client = await SetupHttpClient(Roles.Reader, culture, version);

            // When
            var response = await client.PutAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version) + $"/{id}", putRequestPayload);

            // Then
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task<HttpClient> SetupHttpClient(string role, string culture, string version)
        {
            var client = factory.CreateClient();
            var authResponse = await client.PostAsync(string.Format(SampleDataV1.readerLoginEndpoint, culture, version, Roles.Reader), null);
            var authDetails = JsonConvert.DeserializeObject<AuthenticateResponse>(await authResponse.Content.ReadAsStringAsync());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authDetails.Token);
            return client;
        }

        public void Dispose()
        {
            factory = null;
        }
    }
}