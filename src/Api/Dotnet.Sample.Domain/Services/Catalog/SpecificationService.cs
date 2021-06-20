using Dotnet.Sample.Api.Domain.Models.Catalog;
using Dotnet.Sample.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Domain.Services.Catalog
{
    public interface ISpecificationService
    {
        /// <summary>
        /// Get all specifications
        /// </summary>
        /// <returns>List of specification entities</returns>
        IList<Specification> GetAllSpecifications();

        /// <summary>
        /// Get specification by id
        /// </summary>
        /// <param name="id">Specification id</param>
        /// <returns>Specification entity</returns>
        Specification GetSpecificationById(Guid id);

        /// <summary>
        /// Insert specification
        /// </summary>
        /// <param name="specification">Specification entity</param>
        Task InsertSpecification(Specification specification);

        /// <summary>
        /// Update specification
        /// </summary>
        /// <param name="specification">Specification entity</param>
        Task UpdateSpecification(Specification specification);

        /// <summary>
        /// Delete specifications
        /// </summary>
        /// <param name="ids">List of specification ids</param>
        Task DeleteSpecifications(IList<Guid> ids);

        /// <summary>
        /// Insert product specification mappings
        /// </summary>
        /// <param name="productSpecificationMappings">Product specification mappings</param>
        Task InsertProductSpecificationMappings(IList<ProductSpecificationMapping> productSpecificationMappings);

        /// <summary>
        /// Delete all product specification by product id
        /// </summary>
        /// <param name="productId">Product id</param>
        Task DeleteAllProductSpecificationMappings(Guid productId);
    }
    public class SpecificationService : ISpecificationService
    {
        #region Fields

        private readonly IDatabase _context;

        #endregion

        #region Constructor

        public SpecificationService(IDatabase context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all specifications
        /// </summary>
        /// <returns>List of specification entities</returns>
        public IList<Specification> GetAllSpecifications()
        {
            var entities = _context.Specifications.OrderBy(x => x.Name).ToList();

            return entities;
        }

        /// <summary>
        /// Get specification by id
        /// </summary>
        /// <param name="id">Specification id</param>
        /// <returns>Specification entity</returns>
        public Specification GetSpecificationById(Guid id)
        {
            return _context.Specifications.Find(id);
        }

        /// <summary>
        /// Insert specification
        /// </summary>
        /// <param name="specification">Specification entity</param>
        public async Task InsertSpecification(Specification specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            _context.Specifications.Add(specification);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update specification
        /// </summary>
        /// <param name="specification">Specification entity</param>
        public async Task UpdateSpecification(Specification specification)
        {
            if (specification == null)
                throw new ArgumentNullException(nameof(specification));

            _context.Specifications.Update(specification);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete specifications
        /// </summary>
        /// <param name="ids">List of specification ids</param>
        public async Task DeleteSpecifications(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            foreach (var id in ids)
                _context.Specifications.Remove(GetSpecificationById(id));

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Insert product specification mappings
        /// </summary>
        /// <param name="productSpecificationMappings">Product specification mappings</param>
        public async Task InsertProductSpecificationMappings(IList<ProductSpecificationMapping> productSpecificationMappings)
        {
            if (productSpecificationMappings == null)
                throw new ArgumentNullException(nameof(productSpecificationMappings));

            foreach (var mapping in productSpecificationMappings)
                _context.ProductSpecificationMappings.Add(mapping);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete all product specification by product id
        /// </summary>
        /// <param name="productId">Product id</param>
        public async Task DeleteAllProductSpecificationMappings(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));

            var mappings = _context.ProductSpecificationMappings.Where(x => x.ProductId == productId);

            foreach (var mapping in mappings)
                _context.ProductSpecificationMappings.Remove(mapping);

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
