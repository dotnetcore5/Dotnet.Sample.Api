﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xero.Demo.Api.Datastore;
using Xero.Demo.Api.Domain.Models;
using Xero.Demo.Api.Endpoints.V2.Products;
using Xero.Demo.Api.Tests.EndpointTests.UnitTests.V2.TestData;
using Xunit;

namespace Xero.Demo.Api.Tests.EndpointTests.UnitTests.V2.Products
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
            var actualResponsePayload = actualResponse.Value as ProductDTO;

            //Then
            Assert.NotNull(actualResponse);
            Assert.Equal(StatusCodes.Status201Created, actualResponse.StatusCode);
            Assert.Equal(CONSTANTS.RouteNames.GetByIdAsync, actualResponse.RouteName);
            Assert.Equal(SampleDataV2.Product.Name, actualResponsePayload.Name);
        }

        public void Dispose()
        {
            options = null;
        }
    }
}