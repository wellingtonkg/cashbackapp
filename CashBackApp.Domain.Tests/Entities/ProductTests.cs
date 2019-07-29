using CashBackApp.Domain.Entities;
using System;
using Xunit;

namespace CashBackApp.Domain.Tests.Entities
{
    public class ProductTests
    {
        Guid id = Guid.NewGuid();
        string name = "Disks 01";
        double price = 10;


        [Fact]
        public void New_Product_IsValid()
        {
            var Product = new Product { Id = id, Name = name, Price = price};

            Assert.NotNull(Product);
            Assert.True(Product.IsValid());
            Assert.Equal(id, Product.Id);
            Assert.Equal(name, Product.Name);
        }
    }
}
