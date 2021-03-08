using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Xero.Demo.Api.Domain;
using Xero.Demo.Api.Domain.Models;
using Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.TestData;
using Xero.Demo.Api.Tests.Setup;
using Xunit;

namespace Xero.Demo.Api.Tests.EndpointTests.IntegrationTests
{
    public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock) { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new[] { new Claim(ClaimTypes.Role, CONSTANTS.Roles.Reader) };
            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }

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
            var client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication("Test")
                        .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                            "Test", options => { });
                });
            })
               .CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

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