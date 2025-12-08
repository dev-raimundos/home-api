using HomeApi.Domains.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeApi.Infrastructure.Database.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("USERS");

            // Chave primária no Oracle
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.Name)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(u => u.Email)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(u => u.PasswordHash)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(20);

            builder.Property(u => u.EmailConfirmed)
                   .IsRequired();

            builder.Property(u => u.Active)
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .IsRequired();

            builder.Property(u => u.UpdatedAt);

            builder.HasIndex(u => u.Email)
                   .IsUnique();
        }
    }
}
