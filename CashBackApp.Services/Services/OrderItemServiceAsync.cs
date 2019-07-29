using CashBackApp.Domain.Entities;
using CashBackApp.Repositories.Abstract;
using CashBackApp.Services.Abstract;
using System;

namespace CashBackApp.Services.Services
{
    public class OrderItemServiceAsync : EntityBaseServiceAsync<OrderItem>, IOrderItemServiceAsync
    {
        private IOrderItemRepositoryAsync _repostory;

        public OrderItemServiceAsync(IOrderItemRepositoryAsync repostory) : base(repostory)
        {
            _repostory = repostory;
        }        
    }
}
