using Dotnet.Sample.Api.Domain.Models.Catalog;
using Dotnet.Sample.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Domain.Services.Catalog
{
    public interface IReviewService
    {
        /// <summary>
        /// Get review using product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        IList<Review> GetReviewsByProductId(Guid productId);

        /// <summary>
        /// Get review using product id and user id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Review GetReviewByProductIdUserId(Guid productId, Guid userId);

        /// <summary>
        /// Insert review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        Task InsertReview(Review review);

        /// <summary>
        /// Update review
        /// </summary>
        /// <param name="review"></param>
        Task UpdateReview(Review review);
    }
    public class ReviewService : IReviewService
    {
        #region Fields

        private readonly IDatabase _context;

        #endregion

        #region Constructor

        public ReviewService(IDatabase context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get review using product id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IList<Review> GetReviewsByProductId(Guid productId)
        {
            if (productId == Guid.Empty)
                return null;

            return _context.Reviews.Where(x => x.ProductId == productId).ToList();
        }

        /// <summary>
        /// Get review using product id and user id
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Review GetReviewByProductIdUserId(Guid productId, Guid userId)
        {
            if (productId == Guid.Empty || userId == Guid.Empty)
                return null;

            return _context.Reviews.FirstOrDefault(x => x.ProductId == productId && x.UserId == userId);
        }

        /// <summary>
        /// Insert review
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        public async Task InsertReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update review
        /// </summary>
        /// <param name="review"></param>
        public async Task UpdateReview(Review review)
        {
            if (review == null)
                throw new ArgumentNullException(nameof(review));

            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
