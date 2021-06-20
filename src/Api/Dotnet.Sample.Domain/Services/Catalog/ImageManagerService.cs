using Dotnet.Sample.Api.Domain.Models.Catalog;
using Dotnet.Sample.Datastore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Domain.Services.Catalog
{
    public interface IImageManagerService
    {
        Task<IList<Image>> GetAllImages();
        Task<Image> GetImageById(Guid id);
        Task<IList<Image>> SearchImages(string keyword);
        Task InsertImages(IList<Image> images);
        Task DeleteImages(IList<Guid> ids);
        Task InsertProductImageMappings(IList<ProductImageMapping> productImageMappings);
        Task DeleteAllProductImageMappings(Guid productId);
    }
    public class ImageManagerService : IImageManagerService
    {
        #region Fields
        private readonly IDatabase _context;

        #endregion

        #region Constructor

        public ImageManagerService(IDatabase context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all images
        /// </summary>
        /// <returns>List of image entities</returns>
        public async Task< IList<Image>> GetAllImages()
        {
            var entites = await _context.Images.OrderBy(x => x.FileName).ToListAsync();

            return entites;
        }

        /// <summary>
        /// Get Image using Id
        /// </summary>
        /// <param name="id">Image id</param>
        /// <returns>Image entity</returns>
        public async Task<Image> GetImageById(Guid id)
        {
            return await _context.Images.FindAsync(id);
        }

        /// <summary>
        /// Search images
        /// </summary>
        /// <param name="keyword">keyword</param>
        /// <returns>List of image entities</returns>
        public async Task< IList<Image>> SearchImages(string keyword)
        {
            return await _context.Images.Where(x => x.FileName.Contains(keyword))
                .OrderBy(x => x.FileName)
                .ToListAsync();
        }

        /// <summary>
        /// Insert image
        /// </summary>
        /// <param name="images">List of image entities to insert</param>
        public async Task InsertImages(IList<Image> images)
        {
            if (images == null)
                throw new ArgumentNullException(nameof(images));

            foreach (var image in images)
                _context.Images.Add(image);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="ids">Ids of image entities to delete</param>
        public async Task DeleteImages(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            foreach (var id in ids)
                _context.Images.Remove(await GetImageById(id));

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Insert product image mapping
        /// </summary>
        /// <param name="productImageMappings">List of product image mapping</param>
        public async Task InsertProductImageMappings(IList<ProductImageMapping> productImageMappings)
        {
            if (productImageMappings == null)
                throw new ArgumentNullException(nameof(productImageMappings));

            foreach (var mapping in productImageMappings)
                _context.ProductImageMappings.Add(mapping);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete product image mapping
        /// </summary>
        /// <param name="productId">Product id</param>
        public async Task DeleteAllProductImageMappings(Guid productId)
        {
            if (productId == Guid.Empty)
                throw new ArgumentNullException(nameof(productId));

            var mappings = _context.ProductImageMappings.Where(x => x.ProductId == productId);

            foreach (var mapping in mappings)
                _context.ProductImageMappings.Remove(mapping);

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
