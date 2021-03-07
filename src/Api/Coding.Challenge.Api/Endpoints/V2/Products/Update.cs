using Xero.Demo.Api.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;

namespace Xero.Demo.Api.Endpoints.V2.Products
{
    public partial class ProductsController
    {
        /// <summary>
        /// TO-DO :: Edit product.
        /// </summary>
        /// <param name="id">Enter the product id</param>
        /// <param name="product">Enter the product</param>
        /// <returns></returns>
        [ApiVersion(ApiVersionNumbers.V2)]
        [HttpPut("{id}", Name = RouteNames.PutAsync)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(Guid id, Product product)
        {
            // simulating asynchronous/ non-blocking call
            await Task.Delay(1);
            return NoContent();
        }
    }
}