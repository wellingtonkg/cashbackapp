using System;
using System.Collections.Generic;
using System.Linq;
using CashBackApp.Database.Context;
using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using CashBackApp.Repositories.Abstract;

namespace CashBackApp.Repositories.Repositories
{
    public class OrderItemRepositoryAsync : EntityBaseRepositoryAsync<OrderItem>, IOrderItemRepositoryAsync
    {
        private Context _context;
        private IProductRepositoryAsync _productRepositoryAsync;
        private ICashbackSettingsRepositoryAsync _cashbackSettingsRepositoryAsync;

        public OrderItemRepositoryAsync(
            Context context,
            IProductRepositoryAsync productRepositoryAsync,
            ICashbackSettingsRepositoryAsync cashbackSettingsRepositoryAsync) : base(context)
        {
            _context = context;
            _productRepositoryAsync = productRepositoryAsync;
            _cashbackSettingsRepositoryAsync = cashbackSettingsRepositoryAsync;
        }

        /// <summary>
        /// Calculate the values and cashback for OrderItem
        /// </summary>
        /// <param name="companyId">company Id</param>
        /// <param name="date">Order Date</param>
        /// <param name="orderItem">Order Item</param>
        public async void CalculateItem(Guid companyId, DateTime date, OrderItem orderItem)
        {
            var product = await _productRepositoryAsync.GetSingleAsync(orderItem.ProductId);
            if (product != null)
            {
                orderItem.Value = product.Price;
            }

            //Get current cashback value
            var cashbackSetting = _cashbackSettingsRepositoryAsync.GetCashbackByGenreAndDay(companyId, product.Genre, date.DayOfWeek);
            if (cashbackSetting != null)
            {
                orderItem.CashbackSettingsId = cashbackSetting.Id;
                orderItem.CashbackValue = Math.Round((orderItem.Total * cashbackSetting.Percentage) / 100, 2);
            }
        }
    }
}
