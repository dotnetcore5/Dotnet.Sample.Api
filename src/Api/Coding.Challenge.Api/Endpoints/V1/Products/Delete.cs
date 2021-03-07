using Rest.Api.Domain.Extension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Rest.Api.Domain.Models.CONSTANTS;

namespace Rest.Api.Endpoints.V1.Products
{
    public partial class ProductsController
    {
        private static int rowCountDeleted = 0;
        private readonly bool deleted = rowCountDeleted != 1;

        /// <summary>
        /// Delete product.
        /// </summary>
        /// <param name="id">Enter the product id</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpDelete("{id}", Name = RouteNames.DeleteAsync)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id, string culture = "en-US")
        {
            if (!ModelState.IsValid || id == Guid.Empty) return BadRequest(ModelState.GetErrorMessages());

            var traceIdentifier = HttpContext.TraceIdentifier;
            _logger.LogInformation(string.Format(LogMessage.PreRequestLog, nameof(DeleteAsync), id, traceIdentifier));

            var product = await _db.Products.FindAsync(id);

            if (product == default) return NotFound(string.Format(CustomException.NotFoundException, id));

            var productRemoved = _db.Products.Remove(product);
            rowCountDeleted = await _db.SaveAsync();

            _logger.LogInformation(string.Format(LogMessage.PostRequestLog, nameof(DeleteAsync), id, traceIdentifier));

            return deleted ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}