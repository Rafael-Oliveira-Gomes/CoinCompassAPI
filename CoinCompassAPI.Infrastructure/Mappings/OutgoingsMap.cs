using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinCompassAPI.Domain.Entities.Mappings
{
    public class OutgoingsMap : IEntityTypeConfiguration<Outgoings>
    {
        public void Configure(EntityTypeBuilder<Outgoings> builder)
        {
            builder.ToTable("Outgoings");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(o => o.UserId)
                .IsRequired();

            builder.Property(o => o.TypeOutgoings)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(o => o.Date)
                .IsRequired();

            builder.Property(o => o.Description)
                .HasMaxLength(200);

            builder.Property(o => o.HowPaid)
                .HasMaxLength(50);

            builder.Property(o => o.AmountOutGoings)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
