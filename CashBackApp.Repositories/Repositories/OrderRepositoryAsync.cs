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
