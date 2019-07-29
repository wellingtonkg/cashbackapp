using CashBackApp.Domain.Entities;
using CashBackApp.Domain.Enums;
using System;
using Xunit;

namespace CashBackApp.Domain.Tests.Entities
{
    public class OrderTests
    {
        Guid id = Guid.NewGuid();
        Guid companyId = Guid.NewGuid();
        Guid customerId = Guid.NewGuid();
        double total = 100;
        double cashbackTotal = 0;

        [Fact]
        public void New_Order_IsValid()
        {
            var order = new Order
            {
                CompanyId = companyId,
                CustomerId = customerId,
                Total = total,
                CashbackTotal = cashbackTotal
            };

            order.OrderItems.Add(new OrderItem());

            Assert.NotNull(order);
            Assert.True(order.IsValid());
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void Invalid_Order_Total_Must_Fail(double totalValid)
        {
            Assert.Throws<ArgumentException>(() => new Order { CompanyId = companyId, CustomerId = customerId, Total = totalValid, CashbackTotal = cashbackTotal }.IsValid());
        }

        [Fact]
        public void Invalid_Order_ExistsOrderItem_Must_Fail()
        {
            Assert.Throws<ArgumentException>(() => new Order { CompanyId = companyId, CustomerId = customerId, Total = total, CashbackTotal = cashbackTotal }.IsValid());
        }
    }
}
