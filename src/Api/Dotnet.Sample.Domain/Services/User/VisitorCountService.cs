using Dotnet.Portal.Domain.Statistics;
using Dotnet.Sample.Datastore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Api.Domain.Services.User
{
    public interface IVisitorCountService
    {
        /// <summary>
        /// Get all VisitorCount
        /// </summary>
        /// <returns>VisitorCount entity</returns>
        IList<VisitorCount> GetAllVisitorCount();

        /// <summary>
        /// Get all VisitorCount
        /// </summary>
        /// <param name="take">Number of date to return</param>
        /// <returns>VisitorCount entity</returns>
        IList<VisitorCount> GetAllVisitorCount(int take);

        /// <summary>
        /// Get VisitorCount by date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>VisitorCount entity</returns>
        VisitorCount GetVisitorCountByDate(DateTime date);

        /// <summary>
        /// Insert VisitorCount entity
        /// </summary>
        /// <param name="visitorCount">VisitorCount entity</param>
        Task InsertVisitorCount(VisitorCount visitorCount);

        /// <summary>
        /// Update VisitorCount entity 
        /// </summary>
        /// <param name="visitorCount">VisitorCount entity</param>
        Task UpdateVisitorCount(VisitorCount visitorCount);
    }
    public class VisitorCountService : IVisitorCountService
    {
        #region Fields

        private readonly IDatabase _context;

        #endregion

        #region Constructor

        public VisitorCountService(IDatabase context)
        {
            _context = context;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get all VisitorCount
        /// </summary>
        /// <returns>VisitorCount entity</returns>
        public IList<VisitorCount> GetAllVisitorCount()
        {
            return _context.VisitorCounts.ToList();
        }

        /// <summary>
        /// Get all VisitorCount
        /// </summary>
        /// <param name="take">Number of date to return</param>
        /// <returns>VisitorCount entity</returns>
        public IList<VisitorCount> GetAllVisitorCount(int take)
        {
            return _context.VisitorCounts.Take(take).ToList(); ;
        }

        /// <summary>
        /// Get VisitorCount by date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>VisitorCount entity</returns>
        public VisitorCount GetVisitorCountByDate(DateTime date)
        {
            //return _visitorCountRepository.FindByExpression(x => x.Date == date.Date);
            return _context.VisitorCounts.SingleOrDefault(x => x.Date == date.Date);
        }

        /// <summary>
        /// Insert VisitorCount entity
        /// </summary>
        /// <param name="visitorCount">VisitorCount entity</param>
        public async Task InsertVisitorCount(VisitorCount visitorCount)
        {
            if (visitorCount == null)
                throw new ArgumentNullException(nameof(visitorCount));

            visitorCount.Date = visitorCount.Date.Date;

            _context.VisitorCounts.Add(visitorCount);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update VisitorCount entity 
        /// </summary>
        /// <param name="visitorCount">VisitorCount entity</param>
        public async Task UpdateVisitorCount(VisitorCount visitorCount)
        {
            if (visitorCount == null)
                throw new ArgumentNullException(nameof(visitorCount));

            visitorCount.Date = visitorCount.Date.Date;
            visitorCount.ViewCount++;

            _context.VisitorCounts.Update(visitorCount);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
