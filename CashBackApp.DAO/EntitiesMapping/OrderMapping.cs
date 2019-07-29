using CashBackApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashBackApp.Database.EntitiesMapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.CompanyId).IsRequired();
            builder.Property(m => m.CustomerId).IsRequired();
            builder.Property(m => m.Date).IsRequired();
            builder.Property(m => m.Total)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(m => m.CashbackTotal)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.HasOne(o => o.Company)
                .WithMany();

            builder.HasOne(o => o.Customer)
                .WithMany();
        }
    }
}
