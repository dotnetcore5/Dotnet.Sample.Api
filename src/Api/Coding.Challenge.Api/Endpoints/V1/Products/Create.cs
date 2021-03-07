﻿using Rest.Api.Domain;
using Rest.Api.Domain.Extension;
using Rest.Api.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.FeatureManagement.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Rest.Api.Domain.Models.CONSTANTS;
using Rest.Api.Datastore;

namespace Rest.Api.Endpoints.V1.Products
{
    public partial class ProductsController : BaseApiController
    {
        public readonly Database _db;
        public readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, Database db)
        {
            _logger = logger;
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

            var traceIdentifier = HttpContext.TraceIdentifier;
            _logger.LogInformation(string.Format(LogMessage.PreRequestLog, nameof(PostAsync), product.Id, traceIdentifier));

            var entity = await _db.Products.AddAsync(product);
            var count = await _db.SaveChangesAsync();

            _logger.LogInformation(string.Format(LogMessage.PostRequestLog, nameof(PostAsync), entity.Entity.Id, traceIdentifier));
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