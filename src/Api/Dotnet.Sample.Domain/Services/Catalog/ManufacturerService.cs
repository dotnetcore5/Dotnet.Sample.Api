using Dotnet.Sample.Api.Domain.Models.Catalog;
using Dotnet.Sample.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Domain.Services.Catalog
{
    public interface IManufacturerService
    {
        /// <summary>
        /// Get all manufacturers
        /// </summary>
        /// <returns>List of manufacturer entities</returns>
        IList<Manufacturer> GetAllManufacturers();

        /// <summary>
        /// Get manufacturer using id
        /// </summary>
        /// <param name="id">Manufacturer id</param>
        /// <returns>Manufacturer entity</returns>
        Manufacturer GetManufacturerById(Guid id);

        /// <summary>
        /// Get manufacturer using seo
        /// </summary>
        /// <param name="seo">Manufacturer SEO</param>
        /// <returns>Manufacturer entity</returns>
        Manufacturer GetManufacturerBySeo(string seo);

        /// <summary>
        /// Insert manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer entity</param>
        Task InsertManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Update manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer entity</param>
        Task UpdateManufacturer(Manufacturer manufacturer);

        /// <summary>
        /// Delete manufacturers
        /// </summary>
        /// <param name="ids">Ids of manufacturer to delete</param>
        Task DeleteManufacturers(IList<Guid> ids);

        /// <summary>
        /// Insert product manufacturer mappings
        /// </summary>
        /// <param name="productManufacturerMappings">List of product manufacturer</param>
        Task InsertProductManufacturerMappings(IList<ProductManufacturerMapping> productManufacturerMappings);

        /// <summary>
        /// Delete all product manufacturer mappings using product id
        /// </summary>
        /// <param name="productId">Product id</param>
        Task DeleteAllProductManufacturersMappings(Guid productId);
    }
    public class ManufacturerService : IManufacturerService
    {
        #region Fields

        private readonly IDatabase _context;

        #endregion

        #region Constructor

        public ManufacturerService(IDatabase context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all manufacturers
        /// </summary>
        /// <returns>List of manufacturer entities</returns>
        public IList<Manufacturer> GetAllManufacturers()
        {
            var entities = _context.Manufacturers.OrderBy(x => x.Name).ToList();

            return entities;
        }

        /// <summary>
        /// Get manufacturer using id
        /// </summary>
        /// <param name="id">Manufacturer id</param>
        /// <returns>Manufacturer entity</returns>
        public Manufacturer GetManufacturerById(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return _context.Manufacturers.Find(id);
        }

        /// <summary>
        /// Get manufacturer using seo
        /// </summary>
        /// <param name="seo">Manufacturer SEO</param>
        /// <returns>Manufacturer entity</returns>
        public Manufacturer GetManufacturerBySeo(string seo)
        {
            if (seo == string.Empty)
                return null;

            return _context.Manufacturers.FirstOrDefault(x => x.SeoUrl == seo);
        }

        /// <summary>
        /// Insert manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer entity</param>
        public async Task InsertManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update manufacturer
        /// </summary>
        /// <param name="manufacturer">Manufacturer entity</param>
        public async Task UpdateManufacturer(Manufacturer manufacturer)
        {
            if (manufacturer == null)
                throw new ArgumentNullException(nameof(manufacturer));

            _context.Manufacturers.Update(manufacturer);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Delete manufacturers
        /// </summary>
        /// <param name="ids">Ids of manufacturer to delete</param>
        public async Task DeleteManufacturers(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            foreach (var id in ids)
                _context.Manufacturers.Remove(GetManufacturerById(id));
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Insert product manufacturer mappings
        /// </summary>
        /// <param name="productManufacturerMappings">List of product manufacturer</param>
        public async Task InsertProductManufacturerMappings(IList<ProductManufacturerMapping> productManufacturerMappings)
        {
            if (productManufacturerMappings == null)
                throw new ArgumentNullException(nameof(productManufacturerMappings));

            foreach (var mapping in productManufacturerMappings)
                _context.ProductManufacturerMappings.Add(mapping);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Delete all product manufacturer mappings using product id
        /// </summary>
        /// <param name="productId">Product id</param>
        public async Task DeleteAllProductManufacturersMappings(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));

            var mappings = _context.ProductManufacturerMappings.Where(x => x.ProductId == productId);

            foreach (var mapping in mappings)
                _context.ProductManufacturerMappings.Remove(mapping);
            await _context.SaveChangesAsync();

        }

        #endregion
    }
}
