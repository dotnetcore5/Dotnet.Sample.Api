﻿using Xero.Demo.Api.Domain;
using Xero.Demo.Api.Domain.Extension;
using Xero.Demo.Api.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Xero.Demo.Api.Domain.Models.CONSTANTS;
using Xero.Demo.Api.Datastore;

namespace Xero.Demo.Api.Endpoints.V1.Products
{
    public partial class ProductsController : BaseApiController
    {
        public readonly Database _db;

        public ProductsController(Database db)
        {
            _db = db;
        }

        /// <summary>
        /// Add product.
        /// </summary>
        /// <param name="product">Enter the product</param>
        /// <param name="culture">Enter the culture</param>
        /// <returns></returns>
        //[Authorize("ShouldContainRole")]
        [FeatureGate(Features.PRODUCT)]
        [ApiVersion(ApiVersionNumbers.V1)]
        [HttpPost("", Name = RouteNames.PostAsync)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAsync(Product product, string culture = "en-US")
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.GetErrorMessages());

            var entity = await _db.Products.AddAsync(product);
            var count = await _db.SaveChangesAsync();

            var responseProduct = new ProductDTO
            {
                DeliveryPrice = entity.Entity.DeliveryPrice,
                Description = entity.Entity.Description,
                Id = entity.Entity.Id,
                Name = entity.Entity.Name,
                Price = entity.Entity.Price
            };
            return count == 1 ? CreatedAtRoute(RouteNames.GetByIdAsync, new { id = responseProduct.Id, culture }, responseProduct) : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}