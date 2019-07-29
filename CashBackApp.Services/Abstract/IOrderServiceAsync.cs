using CashBackApp.Domain.Entities;
using System;
using System.Collections.Generic;

namespace CashBackApp.Services.Abstract
{
    public interface IOrderServiceAsync : IEntityBaseServiceAsync<Order>
    {
        List<Order> GetOrders(DateTime startDate, DateTime endDate, int pageSize, int page, out int recordCount);
    }
}
