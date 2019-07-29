using CashBackApp.Domain.Entities;
using CashBackApp.Database.EntitiesMapping;
using Microsoft.EntityFrameworkCore;

namespace CashBackApp.Database.Context
{
    public class Context : DbContext
    {
        public DbSet<CashbackSettings> CashbackSettings { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }

        public Context(DbContextOptions<Context> options) :
                   base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CashbackSettingsMapping());
            modelBuilder.ApplyConfiguration(new CompanyMapping());
            modelBuilder.ApplyConfiguration(new CustomerMapping());
            modelBuilder.ApplyConfiguration(new OrderItemMapping());
            modelBuilder.ApplyConfiguration(new OrderMapping());
            modelBuilder.ApplyConfiguration(new ProductMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
