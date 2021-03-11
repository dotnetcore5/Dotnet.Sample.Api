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
        /// TO-DO :: Delete product.
        /// </summary>
        /// <param name="id">Enter the product id</param>
        /// <returns></returns>
        [ApiVersion(ApiVersionNumbers.V2)]
        [HttpDelete("{id}", Name = RouteNames.DeleteAsync)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await Task.Delay(1).ConfigureAwait(false);                                                                                        // simulating asynchronous/ non-blocking call
            return NoContent();
        }
    }
}