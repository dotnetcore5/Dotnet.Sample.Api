using Dotnet.Sample.Api.Domain.Models.Catalog;
using Dotnet.Sample.Api.Domain.ViewModels;
using Dotnet.Sample.Domain.Models;
using Dotnet.Sample.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Api.Endpoints.V1.Categories
{
    public partial class CategoriesController
    {
        /// <summary>
        /// Update products by sending valid JWT token provided wither through 'api/en-US/v1/Login/Admin' or 'api/en-US/v1/Login/Editor'
        /// </summary>
        /// <param name="id">Enter the category id</param>
        /// <param name="category">Enter the category</param>
        /// <param name="culture"></param>
        /// <returns></returns>
        [Authorize(Policy = Policy.ShouldBeAnEditor)]
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpPut("{id}", Name = RouteNames.PutCatogoryAsync)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> PutCatogoryAsync(Guid id, Category category, string culture = "en-US")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var savedCategory = await _db.Categories.FindAsync(id).ConfigureAwait(false);

            if (savedCategory == default)
            {
                return NotFound(string.Format(CustomException.NotFoundException, id));
            }

            _db.Categories.Update(category);

            var count = await _db.SaveChangesAsync();

            return count == 1 ? NoContent() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}