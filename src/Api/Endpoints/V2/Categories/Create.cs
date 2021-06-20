using Dotnet.Sample.Api.Domain.Models.Catalog;
using Dotnet.Sample.Api.Domain.ViewModels;
using Dotnet.Sample.Datastore;
using Dotnet.Sample.Infrastructure.Extensions;
using Dotnet.Sample.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Api.Endpoints.V2.Categories
{
    public partial class CategoriesController : BaseApiController
    {
        private readonly Database _db;
        public CategoriesController(Database db)
        {
            _db = db;
        }
        /// <summary>
        /// Add product by sending valid JWT token provided through only 'api/{culture}/v1/Login/Admin' endpoint
        /// </summary>
        /// <param name="category">Enter the product</param>
        /// <param name="culture">Enter the culture</param>
        /// <returns></returns>
        [Authorize(Policy = Policy.ShouldBeAnAdmin)]
        [FeatureGate(Features.CATEGORIES)]
        [ApiVersion(ApiVersionNumbers.V2)]
        [HttpPost("", Name = RouteNames.AddAsync)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CategoryModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> AddAsync(Category category, string culture = "en-US")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var addedCategory = await _db.Categories.AddAsync(category).ConfigureAwait(false);

            var stateAded = addedCategory.State == Microsoft.EntityFrameworkCore.EntityState.Added;

            var count = await _db.SaveChangesAsync();

            var responseProduct = new CategoryModel
            {
                Name = category.Name,
                SeoUrl = category.SeoUrl
            };
            return Ok();
            //return stateAded && count == 1 ? CreatedAtRoute(RouteNames.GetByIdAsync, new { seo = responseProduct.SeoUrl, culture }, responseProduct) : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
