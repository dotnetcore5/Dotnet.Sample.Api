using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xero.Demo.Api.Domain.Extension;
using Xero.Demo.Api.Domain.Models;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;

namespace Xero.Demo.Api.Endpoints.V1.Products
{
    public partial class ProductsController
    {
        /// <summary>
        /// List of products.
        /// </summary>
        /// <returns>Returns list of products</returns>
        [Authorize(Policy = "ShouldBeAReader")]
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpGet("", Name = RouteNames.GetAsync)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ProductDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(string culture = "en-US")
        {
            //var language = AddLocalizationExtension._e[WELCOME];
            //_logger.LogInformation(string.Format(AddLocalizationExtension._e[WELCOME].Value));

            var products = await _db.Products.AsQueryable().ToListAsync();

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
        /// Get product by id.
        /// </summary>
        /// <param name="id">Enter the id of product</param>
        /// <returns>Returns list of products</returns>
        [Authorize(Policy = "ShouldBeAReader")]
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpGet("{id}", Name = RouteNames.GetByIdAsync)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            if (!ModelState.IsValid || id == Guid.Empty) return BadRequest(ModelState.GetErrorMessages());

            var product = await _db.Products.FindAsync(id);
            if (product == default) return NotFound(string.Format(CustomException.NotFoundException, id));

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