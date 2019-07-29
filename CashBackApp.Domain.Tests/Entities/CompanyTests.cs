using CashBackApp.Domain.Entities;
using System;
using Xunit;

namespace CashBackApp.Domain.Tests.Entities
{
    public class CompanyTests
    {
        Guid id = Guid.NewGuid();
        String name = "E-commerce Disks";

        [Fact]
        public void New_Company_IsValid()
        {
            var company = new Company { Id = id, Name = name};

            Assert.NotNull(company);
            Assert.True(company.IsValid());
            Assert.Equal(id, company.Id);
            Assert.Equal(name, company.Name);
        }
    }
}
