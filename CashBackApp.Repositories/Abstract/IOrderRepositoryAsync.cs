using CashBackApp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CashBackApp.Repositories.Abstract
{
    public interface IOrderRepositoryAsync : IEntityBaseRepositoryAsync<Order>
    {
        List<Order> GetOrders(DateTime startDate, DateTime endDate, int pageSize, int page, out int recordCount);
    }
}
