using Dotnet.Sample.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Api.Endpoints.V1.Categories
{
    public partial class CategoriesController
    {
        /// <summary>
        /// Delete category by sending valid JWT token provided through only 'api/{culture}/v1/Login/Admin' endpoint
        /// </summary>
        /// <param name="id">Enter the category id</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        [Authorize(Policy = Policy.ShouldBeAnAdmin)]
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpDelete("{id}", Name = RouteNames.DeleteCategoryAsync)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> DeleteCategoryAsync(Guid id, string culture = "en-US")
        {
            if (!ModelState.IsValid || id == Guid.Empty)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var category = await _db.Categories.FindAsync(id).ConfigureAwait(false);

            if (category == default)
            {
                return NotFound(string.Format(CustomException.NotFoundException, id));
            }

            var removedProduct = _db.Categories.Remove(category);
            var stateDeleted = removedProduct.State == EntityState.Deleted;

            var rowCountDeleted = await _db.SaveChangesAsync();

            return stateDeleted && rowCountDeleted == 1 ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}