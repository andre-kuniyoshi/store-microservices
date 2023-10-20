using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infra.Data.Mappings
{
    public class PurchaseOrderMapping : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ClientId)
                .IsRequired()
                .HasColumnType("UniqueIdentifier");

            builder.Property(p => p.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Status)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.HasMany(c => c.Products).WithOne(o => o.Order);
            builder.HasOne(c => c.DeliveryAddress).WithOne(o => o.Order).HasForeignKey<DeliveryAddress>(d => d.OrderId);

            builder.ToTable("Orders");
        }
    }
}
