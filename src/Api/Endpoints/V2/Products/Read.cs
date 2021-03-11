using Dotnet.Sample.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Api.Endpoints.V2.Products
{
    public partial class ProductsController
    {
        /// <summary>
        /// TO-DO :: List of products.
        /// </summary>
        /// <returns>Returns list of products</returns>
        [ApiVersion(ApiVersionNumbers.V2)]
        [HttpGet("", Name = RouteNames.GetAsync)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync()
        {
            // simulating asynchronous/ non-blocking call
            await Task.Delay(1).ConfigureAwait(false);
            return Ok(new List<Product> { new Product { Id = Guid.NewGuid() } });
        }

        /// <summary>
        /// TO-DO :: Get product by id.
        /// </summary>
        /// <param name="id">Enter the id of product</param>
        /// <returns>Returns list of products</returns>
        [ApiVersion(ApiVersionNumbers.V2)]
        [HttpGet("{id}", Name = RouteNames.GetByIdAsync)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            // simulating asynchronous/ non-blocking call
            await Task.Delay(1).ConfigureAwait(false);
            return Ok(new Product { });
        }
    }
}