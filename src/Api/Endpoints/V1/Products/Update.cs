using Dotnet.Sample.Domain.Models;
using Dotnet.Sample.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Api.Endpoints.V1.Products
{
    public partial class ProductsController
    {
        /// <summary>
        /// Get products by sending valid JWT token provided wither through 'api/en-US/v1/Login/Admin' or 'api/en-US/v1/Login/Editor'
        /// </summary>
        /// <param name="id">Enter the product id</param>
        /// <param name="product">Enter the product</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        [Authorize(Policy = Policy.ShouldBeAnEditor)]
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpPut("{id}", Name = RouteNames.PutAsync)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutAsync(Guid id, Product product, string culture = "en-US")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var savedProduct = await _db.Products.FindAsync(id).ConfigureAwait(false);

            if (savedProduct == default)
            {
                return NotFound(string.Format(CustomException.NotFoundException, id));
            }

            _db.Products.Update(product);

            var count = await _db.SaveChangesAsync();

            return count == 1 ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}