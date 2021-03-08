using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Xero.Demo.Api.Domain.Extension;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;

namespace Xero.Demo.Api.Endpoints.V1.Products
{
    public partial class ProductsController
    {
        private static int rowCountDeleted = 0;
        private readonly bool deleted = rowCountDeleted != 1;

        /// <summary>
        /// Delete product by sending valid JWT token provided through only 'api/{culture}/v1/Login/Admin' endpoint
        /// </summary>
        /// <param name="id">Enter the product id</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        [Authorize(Policy = Policy.ShouldBeAnAdmin)]
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpDelete("{id}", Name = RouteNames.DeleteAsync)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(Guid id, string culture = "en-US")
        {
            if (!ModelState.IsValid || id == Guid.Empty) return BadRequest(ModelState.GetErrorMessages());

            var product = await _db.Products.FindAsync(id);

            if (product == default) return NotFound(string.Format(CustomException.NotFoundException, id));

            var productRemoved = _db.Products.Remove(product);
            rowCountDeleted = await _db.SaveChangesAsync();

            return deleted ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}