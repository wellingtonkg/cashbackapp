using System;
using System.Collections.Generic;
using System.Linq;
using CashBackApp.Database.Context;
using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using CashBackApp.Repositories.Abstract;

namespace CashBackApp.Repositories.Repositories
{
    public class CustomerRepositoryAsync : EntityBaseRepositoryAsync<Customer>, ICustomerRepositoryAsync
    {
        private Context _context;

        public CustomerRepositoryAsync(Context context) : base(context)
        {
            _context = context;
        }

        public List<Customer> GetCustomers(string search, int pageSize, int page, out int recordCount)
        {
            var query = string.IsNullOrEmpty(search) ? FindBy(null) : FindBy(p => p.Name.ToLower().Contains(search.ToLower()));

            //total of records
            recordCount = query.Count();

            //paginate result
            return query.OrderBy(o => o.Name)
                        .Skip(pageSize * (page - 1))
                        .Take(pageSize)
                        .ToList();
        }
    }
}
