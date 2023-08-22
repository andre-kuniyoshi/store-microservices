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

            builder.Property(p => p.UserName)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.EmailAddress)
                .HasColumnType("varchar(100)");

            builder.Property(p => p.AddressLine)
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Country)
                .HasColumnType("varchar(100)");

            builder.Property(p => p.State)
                .HasColumnType("varchar(100)");

            builder.Property(p => p.ZipCode)
                .HasColumnType("varchar(100)");

            builder.ToTable("Orders");
        }
    }
}
