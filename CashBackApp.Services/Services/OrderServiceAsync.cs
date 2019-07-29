using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashBackApp.Domain.Entities;
using CashBackApp.Repositories.Abstract;
using CashBackApp.Services.Abstract;

namespace CashBackApp.Services.Services
{
    public class OrderServiceAsync : EntityBaseServiceAsync<Order>, IOrderServiceAsync
    {
        private IOrderRepositoryAsync _repostory;
        private IOrderItemRepositoryAsync _orderItemRepositoryAsync;

        public OrderServiceAsync(IOrderRepositoryAsync repostory,
            IOrderItemRepositoryAsync orderItemRepositoryAsync) : base(repostory)
        {
            _repostory = repostory;
            _orderItemRepositoryAsync = orderItemRepositoryAsync;
        }

        public List<Order> GetOrders(DateTime startDate, DateTime endDate, int pageSize, int page, out int recordCount)
        {
            return _repostory.GetOrders(startDate, endDate, pageSize, page, out recordCount);
        }

        public override Task AddAsync(Order entity)
        {
            if(entity == null)
                throw new ArgumentException("Order object is null");

            foreach (var item in entity.OrderItems)
            {
                _orderItemRepositoryAsync.CalculateItem(entity.CompanyId, entity.Date, item);
            }

            CalculateTotal(entity);
            return base.AddAsync(entity);
        }

        private void CalculateTotal(Order order)
        {
            order.Total = order.OrderItems.Sum(s => s.Total);
            order.CashbackTotal = order.OrderItems.Sum(s => s.CashbackValue);
        }
    }
}
