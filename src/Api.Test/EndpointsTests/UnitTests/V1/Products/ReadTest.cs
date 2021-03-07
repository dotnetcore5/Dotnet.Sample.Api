using Xero.Demo.Api.Endpoints.V1.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xero.Demo.Api.Domain.Models;
using Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.TestData;
using Xero.Demo.Api.Datastore;

namespace Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.Products
{
    [Trait("Category", "Unit")]
    public class ReadTest : IDisposable
    {
        private Database moqDatabase;
        private Mock<ILogger<ProductsController>> moqLogger;
        private ProductsController sut;

        public ReadTest()
        {
            moqLogger = new Mock<ILogger<ProductsController>>();
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: SampleDataV1.Database).Options;
            moqDatabase = new Database(options);
            sut = new ProductsController(moqLogger.Object, moqDatabase)
            {
                ControllerContext = new ControllerContext()
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext
            {
                TraceIdentifier = SampleDataV1.TraceIdentifier
            };
        }

        public void Dispose()
        {
            moqDatabase = null;
            moqLogger = null;
            sut = null;
        }

        [Fact]
        public async Task GetAsync_Gets_All_Product_Successfully()
        {
            //Given
            var products = new List<Product>();
            products.Add(SampleDataV1.Product);
            products.Add(SampleDataV1.Product);
            moqDatabase.Products.AddRange(products);                    //adding the products to be database
            await moqDatabase.SaveChangesAsync();                              //saving the product to be deleted

            //When
            var actualResponse = await sut.GetAsync() as OkObjectResult;
            var actualResponsePayload = actualResponse.Value as List<ProductDTO>;

            //Then
            Assert.NotNull(actualResponse);
            Assert.Equal(StatusCodes.Status200OK, actualResponse.StatusCode);
            Assert.Equal(await moqDatabase.Products.CountAsync(), actualResponsePayload.Count);
        }

        [Fact]
        public async Task GetByIdAsync_Gets_All_Product_Successfully()
        {
            //Given
            var product = await moqDatabase.Products.AddAsync(SampleDataV1.Product);      //adding the product to be database
            await moqDatabase.SaveChangesAsync();                                              //saving the product to be deleted

            //When
            var actualResponse = await sut.GetByIdAsync(product.Entity.Id) as OkObjectResult;
            var actualResponsePayload = actualResponse.Value as ProductDTO;

            //Then
            Assert.NotNull(actualResponse);
            Assert.Equal(StatusCodes.Status200OK, actualResponse.StatusCode);
            Assert.Equal(SampleDataV1.Product.Name, actualResponsePayload.Name);
        }
    }
}