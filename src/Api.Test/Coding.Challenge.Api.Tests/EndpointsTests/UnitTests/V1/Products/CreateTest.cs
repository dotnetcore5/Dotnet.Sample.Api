using Xero.Demo.Api.Endpoints.V1.Products;
using Xero.Demo.Api.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.TestData;
using Xero.Demo.Api.Datastore;

namespace Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.Products
{
    [Trait("Category", "Unit")]
    public class CreateTest : IDisposable
    {
        private Mock<ILogger<ProductsController>> moqLogger;
        private DbContextOptions<Database> options;

        public CreateTest()
        {
            moqLogger = new Mock<ILogger<ProductsController>>();
            options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: SampleDataV1.Database).Options;
        }

        [Fact]
        public async Task PostAsync_Adds_Product_Successfully_With_Valid_Product_Details()
        {
            //Given
            using var moqDatabase = new Database(options);
            var sut = new ProductsController(moqLogger.Object, moqDatabase)
            {
                ControllerContext = new ControllerContext()
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext
            {
                TraceIdentifier = SampleDataV1.TraceIdentifier
            };
            var products = await moqDatabase.Products.CountAsync();

            //When
            var actualResponse = await sut.PostAsync(SampleDataV1.Product, "en-us") as CreatedAtRouteResult;
            var actualResponsePayload = actualResponse.Value as Product;

            //Then
            Assert.NotNull(actualResponse);
            Assert.Equal(StatusCodes.Status201Created, actualResponse.StatusCode);
            Assert.Equal(CONSTANTS.RouteNames.GetByIdAsync, actualResponse.RouteName);
        }

        public void Dispose()
        {
            moqLogger = null;
            options = null;
        }
    }
}