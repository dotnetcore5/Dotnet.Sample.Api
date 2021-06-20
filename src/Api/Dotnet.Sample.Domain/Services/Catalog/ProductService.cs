using Dotnet.Sample.Datastore;
using Dotnet.Sample.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Domain.Services.Catalog
{
    public interface IProductService
    {
        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of product entities</returns>
        IList<Product> GetAllProducts();

        /// <summary>
        /// Get product usind id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product entity</returns>
        Product GetProductById(Guid id);

        /// <summary>
        /// Get product using seo
        /// </summary>
        /// <param name="seo">Product SEO</param>
        /// <returns>Product entity</returns>
        Product GetProductBySeo(string seo);

        /// <summary>
        /// Insert product
        /// </summary>
        /// <param name="product">Product entity</param>
        Task InsertProduct(Product product);

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Product entity</param>
        Task UpdateProduct(Product product);

        /// <summary>
        /// Delete products
        /// </summary>
        /// <param name="ids">Ids of product to delete</param>
        Task DeleteProducts(IList<Guid> ids);

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="nameFilter">Name filter</param>
        /// <param name="seoFilter">SEO filter</param>
        /// <param name="categoryFilter">Category filter</param>
        /// <param name="manufacturerFilter">Manufacturer filter</param>
        /// <param name="priceFilter">Price filter</param>
        /// <param name="isPublished">Published filter</param>
        /// <returns>List of product entities</returns>
        IList<Product> SearchProduct(
            string nameFilter = null,
            string seoFilter = null,
            string[] categoryFilter = null,
            string[] manufacturerFilter = null,
            string[] priceFilter = null,
            bool isPublished = true);

        /// <summary>
        /// Get product context table
        /// </summary>
        /// <returns></returns>
        IQueryable<Product> Table();
    }
    public class ProductService : IProductService
    {
        #region Fields

        private readonly IDatabase _context;

        #endregion

        #region Constructor

        public ProductService(IDatabase context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>List of product entities</returns>
        public IList<Product> GetAllProducts()
        {
            // TODO: update when lazy loading is available
            var entities = _context.Products
                .Include(x => x.Categories).ThenInclude(x => x.Category)
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking()
                .ToList();

            return entities;
        }

        /// <summary>
        /// Get product usind id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product entity</returns>
        public Product GetProductById(Guid id)
        {
            if (id == Guid.Empty)
                return null;


            // TODO: update when lazy loading is available
            var entity = _context.Products
                .Include(x => x.Categories).ThenInclude(x => x.Category)
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == id);

            return entity;
        }

        /// <summary>
        /// Get product using seo
        /// </summary>
        /// <param name="seo">Product SEO</param>
        /// <returns>Product entity</returns>
        public Product GetProductBySeo(string seo)
        {
            if (seo == "")
                return null;

            // TODO: update when lazy loading is available
            var entity = _context.Products
                .Include(x => x.Categories).ThenInclude(x => x.Category)
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking()
                .SingleOrDefault(x => x.SeoUrl == seo);

            return entity;
        }

        /// <summary>
        /// Insert product
        /// </summary>
        /// <param name="product">Product entity</param>
        public async Task InsertProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="product">Product entity</param>
        public async Task UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete products
        /// </summary>
        /// <param name="ids">Ids of product to delete</param>
        public async Task DeleteProducts(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            foreach (var id in ids)
                _context.Products.Remove(GetProductById(id));

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Search products
        /// </summary>
        /// <param name="nameFilter">Name filter</param>
        /// <param name="seoFilter">SEO filter</param>
        /// <param name="categoryFilter">Category filter</param>
        /// <param name="manufacturerFilter">Manufacturer filter</param>
        /// <param name="priceFilter">Price filter</param>
        /// <param name="isPublished">Published filter</param>
        /// <returns>List of product entities</returns>
        public IList<Product> SearchProduct(
            string nameFilter = null,
            string seoFilter = null,
            string[] categoryFilter = null,
            string[] manufacturerFilter = null,
            string[] priceFilter = null,
            bool isPublished = true)
        {
            var result = _context.Products
                .Include(x => x.Categories).ThenInclude(x => x.Category)
                .Include(x => x.Images).ThenInclude(x => x.Image)
                .Include(x => x.Manufacturers).ThenInclude(x => x.Manufacturer)
                .Include(x => x.Specifications).ThenInclude(x => x.Specification)
                .AsNoTracking();

            // published filter
            if (isPublished == false)
            {
                result = result.Where(x => x.Published == false);
            }

            // name filter
            if (nameFilter != null && nameFilter.Length > 0)
            {
                result = result.Where(x => x.Name.ToLower().Contains(nameFilter.ToLower()));
            }

            // seo filter
            if (seoFilter != null && seoFilter.Length > 0)
            {
                throw new NotImplementedException();
            }

            // category filter
            if (categoryFilter != null && categoryFilter.Length > 0)
            {
                result = result.Where(x => x
                    .Categories.Select(c => c.Category.Name.ToLower())
                    .Intersect(categoryFilter.Select(cf => cf.ToLower()))
                    .Any()
                );
            }

            // manufacturer filter
            if (manufacturerFilter != null && manufacturerFilter.Length > 0)
            {
                result = result.Where(x => x
                    .Manufacturers
                    .Select(c => c.Manufacturer.Name.ToLower())
                    .Intersect(manufacturerFilter.Select(mf => mf.ToLower()))
                    .Any()
                );
            }

            // price filter
            if (priceFilter != null && priceFilter.Length > 0)
            {
                var tmpResult = new List<Product>();
                foreach (var price in priceFilter)
                {
                    var p = price.Split('-');
                    int minPrice = Int32.Parse(p[0]);
                    int maxPrice = Int32.Parse(p[1]);

                    var r = result.Where(x => x.Price >= minPrice && x.Price <= maxPrice);
                    if (r.Any()) tmpResult.AddRange(r);
                }
                result = tmpResult.AsQueryable();
            }

            return result.ToList();
        }

        /// <summary>
        /// Get product context table
        /// </summary>
        /// <returns></returns>
        public IQueryable<Product> Table()
        {
            return _context.Products;
        }

        #endregion
    }
}
