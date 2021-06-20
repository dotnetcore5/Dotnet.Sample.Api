using Dotnet.Portal.Domain.Statistics;
using Dotnet.Sample.Api.Domain.Models.Sale;
using Dotnet.Sample.Datastore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Api.Domain.Services.User
{
    public interface IOrderService
    {
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>List of order entities</returns>
        IList<Order> GetAllOrders();

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Order entity</returns>
        Order GetOrderById(Guid id);

        /// <summary>
        /// Get order by order id
        /// </summary>
        /// <param name="id">Order number id</param>
        /// <returns>Order entity</returns>
        Order GetOrderByOrderId(string id);

        /// <summary>
        /// Get all orders by user id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of order entities</returns>
        IList<Order> GetAllOrdersByUserId(Guid userId);

        /// <summary>
        /// Insert order
        /// </summary>
        /// <param name="order">Order entity</param>
        Task InsertOrder(Order order);

        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="order">Order entity</param>
        Task UpdateOrder(Order order);

        /// <summary>
        /// Delete all orders
        /// </summary>
        /// <param name="ids">List of order ids</param>
        Task DeleteOrders(IList<Guid> ids);
    }
    public class OrderService : IOrderService
    {
        #region Fields

        private readonly IDatabase _context;
        private readonly IOrderCountService _orderCountService;

        #endregion

        #region Constrcutor

        public OrderService(IDatabase context, IOrderCountService orderCountService)
        {
            _context = context;
            _orderCountService = orderCountService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns>List of order entities</returns>
        public IList<Order> GetAllOrders()
        {
            // TODO: update when lazy loading is available
            var entities = _context.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .ToList();

            return entities;
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Order entity</returns>
        public Order GetOrderById(Guid id)
        {
            // TODO: update when lazy loading is available
            return _context.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .SingleOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Get order by order id
        /// </summary>
        /// <param name="id">Order number id</param>
        /// <returns>Order entity</returns>
        public Order GetOrderByOrderId(string id)
        {
            // TODO: update when lazy loading is available
            return _context.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .SingleOrDefault(x => x.OrderNumber == id);
        }

        /// <summary>
        /// Get all orders by user id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of order entities</returns>
        public IList<Order> GetAllOrdersByUserId(Guid userId)
        {
            // TODO: update when lazy loading is available
            var entities = _context.Orders
                .Include(x => x.Items)
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToList();

            return entities;
        }

        /// <summary>
        /// Insert order
        /// </summary>
        /// <param name="order">Order entity</param>
        public async Task InsertOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // add or update order count
            var orderCountEntity = _orderCountService.GetOrderCountByDate(DateTime.Now);
            if (orderCountEntity != null)
                await _orderCountService.UpdateOrderCount(orderCountEntity);
            else
            {
                var orderCountModel = new OrderCount
                {
                    Date = DateTime.Now,
                    Count = 1
                };
                await _orderCountService.InsertOrderCount(orderCountModel);
            }
        }

        /// <summary>
        /// Update order
        /// </summary>
        /// <param name="order">Order entity</param>
        public async Task UpdateOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Delete all orders
        /// </summary>
        /// <param name="ids">List of order ids</param>
        public async Task DeleteOrders(IList<Guid> ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            foreach (var id in ids)
                _context.Orders.Remove(GetOrderById(id));

            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
