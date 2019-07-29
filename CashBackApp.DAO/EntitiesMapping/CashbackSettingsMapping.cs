using CashBackApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashBackApp.Database.EntitiesMapping
{
    public class CashbackSettingsMapping : IEntityTypeConfiguration<CashbackSettings>
    {
        public void Configure(EntityTypeBuilder<CashbackSettings> builder)
        {
            builder.ToTable("CashbackSettings");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Genre).IsRequired();
            builder.Property(m => m.DayOfWeek).IsRequired();
            builder.Property(m => m.Percentage)
                .HasColumnType("decimal(10, 2)")
                .IsRequired();
        }
    }
}
