using Xero.Demo.Api.Endpoints.V1.Products;
using Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.TestData;
using Xero.Demo.Api.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Xero.Demo.Api.Datastore;

namespace Xero.Demo.Api.Tests.EndpointTests.UnitTests.V1.Products
{
    [Trait("Category", "Unit")]
    public class DeleteTest : IDisposable
    {
        private Database moqDatabase;

        public DeleteTest()
        {
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: SampleDataV1.Database).Options;
            moqDatabase = new Database(options);
        }

        [Fact]
        public async Task DeleteAsync_Deletes_Product_Successfully_With_Valid_Product_Id()
        {
            //Given
            var sut = new ProductsController(moqDatabase)
            {
                ControllerContext = new ControllerContext()
            };
            sut.ControllerContext.HttpContext = new DefaultHttpContext
            {
                TraceIdentifier = SampleDataV1.TraceIdentifier
            };

            var product = await moqDatabase.Products.AddAsync(SampleDataV1.Product);      //adding the product to be database
            await moqDatabase.SaveChangesAsync();                                              //saving the product to be deleted

            //When
            var actualResponse = await sut.DeleteAsync(product.Entity.Id) as NoContentResult;

            //Then
            Assert.NotNull(actualResponse);
            Assert.Equal(StatusCodes.Status204NoContent, actualResponse.StatusCode);
        }

        public void Dispose()
        {
            moqDatabase = null;
        }
    }
}