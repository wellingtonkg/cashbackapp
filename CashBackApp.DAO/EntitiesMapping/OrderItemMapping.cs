using CashBackApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashBackApp.Database.EntitiesMapping
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.ProductId).IsRequired();
            builder.Property(m => m.Value)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();

            builder.Property(m => m.CashbackSettingsId)
                .IsRequired(false);

            builder.Property(m => m.Quantity).IsRequired();

            builder
                .HasOne(s => s.Order)
                .WithMany(s => s.OrderItems)
                .HasForeignKey(s => s.OrderId);
        }
    }
}
