using Dotnet.Sample.Api.Domain.Models.Catalog;
using Dotnet.Sample.Api.Domain.ViewModels;
using Dotnet.Sample.Datastore;
using Dotnet.Sample.Infrastructure.Extensions;
using Dotnet.Sample.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Api.Endpoints.V1.Categories
{
    public partial class CategoriesController
    {
        /// <summary>
        /// Get Categories by sending valid JWT token provided through only 'api/{culture}/v1/Login/Admin' endpoint
        /// </summary>
        /// <param name="culture">Enter the culture</param>
        /// <returns></returns>
        [Authorize(Policy = Policy.ShouldBeAReader)]
        [FeatureGate(Features.CATEGORIES)]
        [HttpGet("", Name = RouteNames.GetCategoriesAsync)]
        [ApiVersion(ApiVersionNumbers.V1)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<IActionResult> GetCategoriesAsync(string culture = "en-US")
        {
            var categories = await _db.Categories.AsQueryable().ToListAsync().ConfigureAwait(false);

            var res = categories.Select(p => new CategoryModel
            {
                SeoUrl = p.SeoUrl,
                Name=p.Name
            }).ToList();

            return Ok(res);
        }
    }
}
