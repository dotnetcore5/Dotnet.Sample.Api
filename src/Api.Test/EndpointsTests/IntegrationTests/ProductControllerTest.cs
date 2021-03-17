using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Dotnet.Sample.Api.Tests.EndpointTests.UnitTests.V1.TestData;
using Dotnet.Sample.Api.Tests.Setup;
using Dotnet.Sample.Domain.Models;
using Xunit;
using Dotnet.Sample.Infrastructure;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Api.Tests.EndpointTests.IntegrationTests
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
            var authResponse = await client.PostAsync(string.Format(SampleDataV1.readerLoginEndpoint, culture, version, Roles.Reader), null).ConfigureAwait(false);
            var strAuthDetails = await authResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var authDetails = JsonConvert.DeserializeObject<AuthenticateResponse>(strAuthDetails);
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
            var authResponse = await client.PostAsync(string.Format(SampleDataV1.readerLoginEndpoint, culture, version, Roles.Admin), null).ConfigureAwait(false);
            var strAuthDetails = await authResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var authDetails = JsonConvert.DeserializeObject<AuthenticateResponse>(strAuthDetails);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authDetails.Token);
            var addProductResponse = await client.PostAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version), SampleDataV1.Product).ConfigureAwait(false);
            var strProduct = await addProductResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var addedProduct = JsonConvert.DeserializeObject<Product>(strProduct);
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
            var client = await SetupHttpClient(Roles.Admin, culture, version).ConfigureAwait(false);

            // When
            var response = await client.PostAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version), SampleDataV1.Product).ConfigureAwait(false);

            // Then
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Theory]
        [InlineData("en-US", "1")]
        public async Task PutAsync_Returns_204(string culture, string version)
        {
            // Given
            var client = await SetupHttpClient(Roles.Admin, culture, version).ConfigureAwait(false);
            await client.PostAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version), SampleDataV1.Product).ConfigureAwait(false);

            var productResponse = await client.GetAsync(string.Format(SampleDataV1.productEndpoint, culture, version)).ConfigureAwait(false);
            var strProduct = await productResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            var products = JsonConvert.DeserializeObject<List<ProductDTO>>(strProduct);

            var id = products.FirstOrDefault().Id;
            var putRequestPayload = new Product
            {
                Id = products.FirstOrDefault().Id,
                Name = "Newname",
                DeliveryPrice = 11,
                Price = 22,
                Description = "newDescription"
            };
            client = await SetupHttpClient(Roles.Editor, culture, version).ConfigureAwait(false);

            // When
            var response = await client.PutAsJsonAsync(string.Format(SampleDataV1.productEndpoint, culture, version) + $"/{id}", putRequestPayload);

            // Then
            Assert.Equal(System.Net.HttpStatusCode.NoContent, response.StatusCode);
        }

        private async Task<HttpClient> SetupHttpClient(string role, string culture, string version)
        {
            var client = factory.CreateClient();
            var authResponse = await client.PostAsync(string.Format(SampleDataV1.readerLoginEndpoint, culture, version, role), null);
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