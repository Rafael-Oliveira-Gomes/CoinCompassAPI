using CoinCompassAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinCompassAPI.Infrastructure.Mappings
{
    public class InvestmentMap : IEntityTypeConfiguration<Investment>
    {
        public void Configure(EntityTypeBuilder<Investment> builder)
        {
            builder.ToTable("Investments");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(i => i.UserId)
                .IsRequired();

            builder.Property(i => i.InvestmentType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(i => i.StartDate)
                .IsRequired();

            builder.Property(i => i.EndDate)
                .IsRequired();

            builder.Property(i => i.InterestRate)
                .IsRequired()
                .HasColumnType("float");
        }
    }
}
