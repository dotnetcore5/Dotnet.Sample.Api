using Dotnet.Sample.Api.Domain.Models.Catalog;
using Dotnet.Sample.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Domain.Services.Catalog
{
    public interface ICategoryService
    {
        IList<Category> GetAllCategories();
        Category GetCategoryById(Guid id);

        Category GetCategoryBySeo(string seo);
        Task InsertCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategories(IList<Guid> ids);
        Task InsertProductCategoryMappings(IList<ProductCategoryMapping> productCategoryMappings);
        Task DeleteAllProductCategoryMappingsByProductId(Guid productId);
    }
    public class CategoryService : ICategoryService
    {
        private readonly IDatabase _context;

        public CategoryService(IDatabase context)
        {
            _context = context;
        }

        public IList<Category> GetAllCategories()
        {
            var entities = _context.Categories.OrderBy(x => x.Name).ToList();

            return entities;
        }
        public Category GetCategoryById(Guid id)
        {
            if (id == Guid.Empty)
                return null;

            return _context.Categories.FirstOrDefault(x => x.Id == id);
        }
        public Category GetCategoryBySeo(string seo)
        {
            if (seo == "")
                return null;

            return _context.Categories.FirstOrDefault(x => x.SeoUrl == seo);
        }
        public async Task InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCategories(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            foreach (var id in ids)
                _context.Categories.Remove(GetCategoryById(id));

            await _context.SaveChangesAsync();
        }
        public async Task InsertProductCategoryMappings(IList<ProductCategoryMapping> productCategoryMappings)
        {
            if (productCategoryMappings == null)
                throw new ArgumentNullException(nameof(productCategoryMappings));

            foreach (var mapping in productCategoryMappings)
                _context.ProductCategoryMappings.Add(mapping);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllProductCategoryMappingsByProductId(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));

            var mappings = _context.ProductCategoryMappings.Where(x => x.ProductId == productId);

            foreach (var mapping in mappings)
                _context.ProductCategoryMappings.Remove(mapping);

            await _context.SaveChangesAsync();
        }
    }
}
