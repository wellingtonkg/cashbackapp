using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using System;
using Xunit;

namespace CashBackApp.Domain.Tests.Entities
{
    public class OrderItemTests
    {
        Guid id = Guid.NewGuid();
        Guid productId = Guid.NewGuid();
        int quantity = 5;
        double total = 1;

        [Fact]
        public void New_OrderItem_IsValid()
        {
            var orderItem = new OrderItem
            {
                ProductId = productId,
                Value = total,
                Quantity = quantity,
                CashbackValue = 0
            };

            Assert.NotNull(orderItem);
            Assert.True(orderItem.IsValid());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void Invalid_OrderItem_CashbackValue_Must_Fail(double cashbackInvalid)
        {
            Assert.Throws<ArgumentException>(() => new OrderItem
            {
                ProductId = productId,
                Value = total,
                Quantity = quantity,
                CashbackValue = cashbackInvalid
            }.IsValid());
        }
    }
}
