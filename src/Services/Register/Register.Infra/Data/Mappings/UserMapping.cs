using Register.Application.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Register.Infra.Data.Mappings
{
    public class RegisterMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Document)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.Ddd)
                .IsRequired()
                .HasColumnType("varchar(3)");

            builder.Property(p => p.Phone)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(p => p.Email)
                .HasColumnType("varchar(100)");

            builder.HasOne(f => f.Address)
                .WithOne(p => p.User);

            builder.ToTable("Register");
        }
    }
}
