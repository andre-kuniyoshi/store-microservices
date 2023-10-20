using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infra.Data.Mappings
{
    public class DeliveryAddressMapping : IEntityTypeConfiguration<DeliveryAddress>
    {
        public void Configure(EntityTypeBuilder<DeliveryAddress> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.OrderId)
                .IsRequired()
                .HasColumnType("UniqueIdentifier");

            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnType("varchar(150)");

            builder.Property(p => p.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.Complement)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.District)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.CEP)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(p => p.City)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.UF)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.ToTable("DeliveryAddress");
        }
    }
}
