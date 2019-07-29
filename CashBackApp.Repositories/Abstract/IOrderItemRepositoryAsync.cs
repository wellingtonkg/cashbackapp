using CashBackApp.Domain.Entities;
using System;

namespace CashBackApp.Repositories.Abstract
{
    public interface IOrderItemRepositoryAsync : IEntityBaseRepositoryAsync<OrderItem>
    {
        void CalculateItem(Guid companyId, DateTime date, OrderItem orderItem);
    }
}
