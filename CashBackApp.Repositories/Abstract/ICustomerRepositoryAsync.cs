using CashBackApp.Domain.Entities;
using System.Collections.Generic;

namespace CashBackApp.Repositories.Abstract
{
    public interface ICustomerRepositoryAsync : IEntityBaseRepositoryAsync<Customer>
    {
        List<Customer> GetCustomers(string search, int pageSize, int page, out int recordCount);
    }
}
