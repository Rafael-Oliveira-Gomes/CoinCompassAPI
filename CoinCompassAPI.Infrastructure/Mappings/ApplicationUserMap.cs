using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CoinCompassAPI.Domain.Entities;

namespace CoinCompassAPI.Infrastructure.Mappings
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(u => u.NormalizedUserName)
                   .HasMaxLength(256);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(256);

            builder.Property(u => u.NormalizedEmail)
                   .HasMaxLength(256);

            builder.Property(u => u.EmailConfirmed)
                   .IsRequired();

            builder.Property(u => u.PasswordHash);

            builder.Property(u => u.SecurityStamp);

            builder.Property(u => u.ConcurrencyStamp)
                   .IsConcurrencyToken();

            builder.Property(u => u.PhoneNumber)
                   .HasMaxLength(15);

            builder.Property(u => u.PhoneNumberConfirmed)
                   .IsRequired();

            builder.Property(u => u.TwoFactorEnabled)
                   .IsRequired();

            builder.Property(u => u.LockoutEnd);

            builder.Property(u => u.LockoutEnabled)
                   .IsRequired();

            builder.Property(u => u.AccessFailedCount)
                   .IsRequired();
        }
    }
}
