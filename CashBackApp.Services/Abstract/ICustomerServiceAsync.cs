using CashBackApp.Domain.Entities;
using System.Collections.Generic;

namespace CashBackApp.Services.Abstract
{
    public interface ICustomerServiceAsync : IEntityBaseServiceAsync<Customer>
    {
        List<Customer> GetCustomers(string search, int pageSize, int page, out int recordCount);
    }
}
