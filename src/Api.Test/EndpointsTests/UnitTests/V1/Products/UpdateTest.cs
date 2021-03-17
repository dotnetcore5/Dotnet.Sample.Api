using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Dotnet.Sample.Api.Endpoints.V1.Products;
using Dotnet.Sample.Api.Tests.EndpointTests.UnitTests.V1.TestData;
using Xunit;
using Dotnet.Sample.Datastore;

namespace Dotnet.Sample.Api.Tests.EndpointTests.UnitTests.V1.Products
{
    [Trait("Category", "Unit")]
    public class UpdateTest : IDisposable
    {
        private Database moqDatabase;

        public UpdateTest()
        {
            var options = new DbContextOptionsBuilder<Database>().UseInMemoryDatabase(databaseName: SampleDataV1.Database).Options;
            moqDatabase = new Database(options);
        }

        [Fact]
        public async Task PutAsync_Updates_Product_Successfully_With_Valid_Product_Id_and_Details()
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

            var product = await moqDatabase.Products.AddAsync(SampleDataV1.Product);            //adding the product to be database
            await moqDatabase.SaveChangesAsync();                                              //saving the product to be updated
            product.Entity.Description = SampleDataV1.NewDescription;

            //When
            var actualResponse = await sut.PutAsync(product.Entity.Id, product.Entity) as NoContentResult;

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