using Rest.Api.Endpoints.V2.Products;
using Rest.Api.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Rest.Api.Tests.EndpointTests.UnitTests.V2.TestData;
using Rest.Api.Datastore;

namespace Rest.Api.Tests.EndpointTests.UnitTests.V2.Products
{
    [Trait("Category", "Unit")]
    public class CreateTest : IDisposable
    {
        private DbContextOptions<Database> options;

        public CreateTest()
        {
            options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: SampleDataV2.Database).Options;
        }

        [Fact]
        public async Task PostAsync_Adds_Product_Successfully_With_Valid_Product_Details()
        {
            //Given
            using var moqDatabase = new Database(options);
            var sut = new ProductsController()
            {
                ControllerContext = new ControllerContext()
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext
            {
                TraceIdentifier = SampleDataV2.TraceIdentifier
            };
            var products = await moqDatabase.Products.CountAsync();

            //When
            var actualResponse = await sut.PostAsync(SampleDataV2.Product, "en-us") as CreatedAtRouteResult;
            var actualResponsePayload = actualResponse.Value as Product;

            //Then
            Assert.NotNull(actualResponse);
            Assert.Equal(StatusCodes.Status201Created, actualResponse.StatusCode);
            Assert.Equal(CONSTANTS.RouteNames.GetByIdAsync, actualResponse.RouteName);
        }

        public void Dispose()
        {
            options = null;
        }
    }
}