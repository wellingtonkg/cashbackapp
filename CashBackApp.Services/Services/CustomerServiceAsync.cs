using CashBackApp.Domain.Entities;
using CashBackApp.Repositories.Abstract;
using CashBackApp.Services.Abstract;
using System.Collections.Generic;

namespace CashBackApp.Services.Services
{
    public class CustomerServiceAsync : EntityBaseServiceAsync<Customer>, ICustomerServiceAsync
    {
        private ICustomerRepositoryAsync _repostory;

        public CustomerServiceAsync(ICustomerRepositoryAsync repostory) : base(repostory)
        {
            _repostory = repostory;
        }

        public List<Customer> GetCustomers(string search, int pageSize, int page, out int recordCount)
        {
            return _repostory.GetCustomers(search, pageSize, page, out recordCount);
        }
    }
}
