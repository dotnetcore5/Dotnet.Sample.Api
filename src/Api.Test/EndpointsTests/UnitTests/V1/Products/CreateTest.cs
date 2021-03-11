using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xero.Demo.Api.Datastore;
using Xero.Demo.Api.Domain.Models;
using Xero.Demo.Api.Endpoints.V1.Products;
using Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.TestData;
using Xunit;

namespace Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.Products
{
    [Trait("Category", "Unit")]
    public class CreateTest : IDisposable
    {
        private DbContextOptions<Database> options;

        public CreateTest()
        {
            options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: SampleDataV1.Database).Options;
        }

        [Fact]
        public async Task PostAsync_Adds_Product_Successfully_With_Valid_Product_Details()
        {
            //Given
            using var moqDatabase = new Database(options);
            var sut = new ProductsController(moqDatabase)
            {
                ControllerContext = new ControllerContext()
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext
            {
                TraceIdentifier = SampleDataV1.TraceIdentifier
            };

            //When
            var actualResponse = await sut.PostAsync(SampleDataV1.Product, "en-us") as CreatedAtRouteResult;
            var actualResponsePayload = actualResponse.Value as ProductDTO;

            //Then
            Assert.NotNull(actualResponse);
            Assert.Equal(StatusCodes.Status201Created, actualResponse.StatusCode);
            Assert.Equal(CONSTANTS.RouteNames.GetByIdAsync, actualResponse.RouteName);
            Assert.Equal(SampleDataV1.Product.Name, actualResponsePayload.Name);
        }

        public void Dispose()
        {
            options = null;
        }
    }
}