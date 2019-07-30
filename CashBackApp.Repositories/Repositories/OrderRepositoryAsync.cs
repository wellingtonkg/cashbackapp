using CashBackApp.Database.Context;
using CashBackApp.Domain.Entities;
using CashBackApp.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CashBackApp.Repositories.Repositories
{
    public class OrderRepositoryAsync : EntityBaseRepositoryAsync<Order>, IOrderRepositoryAsync
    {
        private Context _context;

        public OrderRepositoryAsync(Context context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a list of orders by a range of date and pagination
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <param name="pageSize">Number of items by page</param>
        /// <param name="page">Current Page</param>
        /// <param name="recordCount">Total of records</param>
        /// <returns>List of orders</returns>
        public List<Order> GetOrders(DateTime startDate, DateTime endDate, int pageSize, int page, out int recordCount)
        {
            var query = FindBy(p => p.Date >= startDate && p.Date <= endDate, 
                i => i.Customer,
                i => i.OrderItems);

            //total of records
            recordCount = query.Count();

            //paginate result
            return query.OrderByDescending(o => o.Date)
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToList();
        }
    }
}
