using Dotnet.Portal.Domain.Statistics;
using Dotnet.Sample.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Api.Domain.Services.User
{
    public interface IOrderCountService
    {
        /// <summary>
        /// Get all OrderCount
        /// </summary>
        /// <returns>OrderCount entity</returns>
        IList<OrderCount> GetAllOrderCount();

        /// <summary>
        /// Get all OrderCount
        /// </summary>
        /// <param name="take">Number of date to return</param>
        /// <returns>OrderCount entities</returns>
        IList<OrderCount> GetAllOrderCount(int take);

        /// <summary>
        /// Get OrderCount by date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>OrderCount entity</returns>
        OrderCount GetOrderCountByDate(DateTime date);

        /// <summary>
        /// Insert OrderCount
        /// </summary>
        /// <param name="orderCount">OrderCount entity</param>
        Task InsertOrderCount(OrderCount orderCount);

        /// <summary>
        /// Update OrderCount
        /// </summary>
        /// <param name="orderCount">OrderCount entity</param>
        Task UpdateOrderCount(OrderCount orderCount);
    }
    public class OrderCountService : IOrderCountService
    {
        #region Fields

        private readonly IDatabase _context;

        #endregion

        #region Constructor

        public OrderCountService(IDatabase contexty)
        {
            _context = contexty;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all OrderCount
        /// </summary>
        /// <returns>OrderCount entity</returns>
        public IList<OrderCount> GetAllOrderCount()
        {
            return _context.OrderCounts.ToList();
        }

        /// <summary>
        /// Get all OrderCount
        /// </summary>
        /// <param name="take">Number of date to return</param>
        /// <returns>OrderCount entities</returns>
        public IList<OrderCount> GetAllOrderCount(int take)
        {
            return _context.OrderCounts.Take(take).ToList();
        }

        /// <summary>
        /// Get OrderCount by date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>OrderCount entity</returns>
        public OrderCount GetOrderCountByDate(DateTime date)
        {
            return _context.OrderCounts.FirstOrDefault(x => x.Date == date.Date);
        }

        /// <summary>
        /// Insert OrderCount
        /// </summary>
        /// <param name="orderCount">OrderCount entity</param>
        public async Task InsertOrderCount(OrderCount orderCount)
        {
            if (orderCount == null)
                throw new ArgumentNullException(nameof(orderCount));

            orderCount.Date = orderCount.Date.Date;

            _context.OrderCounts.Add(orderCount);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update OrderCount
        /// </summary>
        /// <param name="orderCount">OrderCount entity</param>
        public async Task UpdateOrderCount(OrderCount orderCount)
        {
            if (orderCount == null)
                throw new ArgumentNullException(nameof(orderCount));

            orderCount.Date = orderCount.Date.Date;
            orderCount.Count++;

            _context.OrderCounts.Update(orderCount);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
