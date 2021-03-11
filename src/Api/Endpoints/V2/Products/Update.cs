using Dotnet.Sample.Domain.Models;
using Dotnet.Sample.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Api.Endpoints.V2.Products
{
    public partial class ProductsController
    {
        /// <summary>
        /// TO-DO :: Edit product.
        /// </summary>
        /// <param name="id">Enter the product id</param>
        /// <param name="product">Enter the product</param>
        /// <param name="culture">Enter the culture</param>
        /// <returns></returns>
        [ApiVersion(ApiVersionNumbers.V2)]
        [HttpPut("{id}", Name = RouteNames.PutAsync)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(Guid id, Product product, string culture)
        {
            if (!ModelState.IsValid && id != product.Id)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }
            // simulating asynchronous/ non-blocking call
            await Task.Delay(1).ConfigureAwait(false);
            return NoContent();
        }
    }
}