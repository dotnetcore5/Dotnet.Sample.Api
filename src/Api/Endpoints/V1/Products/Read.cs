using Dotnet.Sample.Domain.Models;
using Dotnet.Sample.Infrastructure;
using Dotnet.Sample.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dotnet.Sample.Shared.CONSTANTS;

namespace Dotnet.Sample.Api.Endpoints.V1.Products
{
    public partial class ProductsController
    {
        /// <summary>
        /// Get products by sending valid JWT token provided through 'api/{culture}/v1/Login/Admin' or 'api/en-US/v1/Login/Editor' or 'api/{culture}/v1/Login/Reader'
        /// </summary>
        /// <returns>Returns list of products</returns>
        [Authorize(Policy = Policy.ShouldBeAReader)]
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpGet("", Name = RouteNames.GetAsync)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(string culture = "en-US")
        {
            // ### START ::: The localization can be accessed.
            var language = Startup.Localizer["WELCOME"]; //where "WELCOME" is the key
            // ### END ::: The localization can be accessed.
            Console.WriteLine($"{language.Name} : {language.Value}");

            var products = await _db.Products.AsQueryable().ToListAsync().ConfigureAwait(false);

            var res = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                DeliveryPrice = p.DeliveryPrice
            }).ToList();
            return Ok(res);
        }

        /// <summary>
        /// Get product by id by sending valid JWT token provided through 'api/{culture}/v1/Login/Admin' or 'api/en-US/v1/Login/Editor' or 'api/{culture}/v1/Login/Reader'
        /// </summary>
        /// <param name="id">Enter the id of product</param>
        /// <param name="culture"></param>
        /// <returns>Returns list of products</returns>
        [Authorize(Policy = Policy.ShouldBeAReader)]
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpGet("{id}", Name = RouteNames.GetByIdAsync)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id, string culture = "en-US")
        {
            if (!ModelState.IsValid || id == Guid.Empty)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var product = await _db.Products.FindAsync(id).ConfigureAwait(false);
            if (product == default)
            {
                return NotFound(string.Format(CustomException.NotFoundException, id));
            }

            var responseProduct = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                DeliveryPrice = product.DeliveryPrice,
                Price = product.DeliveryPrice
            };

            return Ok(responseProduct);
        }
    }
}