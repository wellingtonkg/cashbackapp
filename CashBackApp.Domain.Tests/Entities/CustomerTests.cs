using CashBackApp.Domain.Entities;
using System;
using Xunit;

namespace CashBackApp.Domain.Tests.Entities
{
    public class CustomerTests
    {
        Guid id = Guid.NewGuid();
        String name = "Wellington";

        [Fact]
        public void New_Customer_IsValid()
        {
            var customer = new Customer { Id = id, Name = name};

            Assert.NotNull(customer);
            Assert.True(customer.IsValid());
            Assert.Equal(id, customer.Id);
            Assert.Equal(name, customer.Name);
        }
    }
}
