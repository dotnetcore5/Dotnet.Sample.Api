using Xero.Demo.Api.Domain.Extension;
using Xero.Demo.Api.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;

namespace Xero.Demo.Api.Endpoints.V1.Products
{
    public partial class ProductsController
    {
        /// <summary>
        /// Edit product.
        /// </summary>
        /// <param name="id">Enter the product id</param>
        /// <param name="product">Enter the product</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpPut("{id}", Name = RouteNames.PutAsync)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(Guid id, Product product, string culture = "en-US")
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

            var traceIdentifier = HttpContext.TraceIdentifier;
            _logger.LogInformation(string.Format(LogMessage.PostRequestLog, nameof(DeleteAsync), id, traceIdentifier));

            var savedProduct = await _db.Products.FindAsync(id);

            if (savedProduct == default) return NotFound(string.Format(CustomException.NotFoundException, id));

            _db.Products.Update(product);
            var count = await _db.SaveChangesAsync();

            _logger.LogInformation(string.Format(LogMessage.PostRequestLog, nameof(DeleteAsync), id, traceIdentifier));

            return count == 1 ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}