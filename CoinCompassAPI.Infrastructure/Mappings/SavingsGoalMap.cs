using CoinCompassAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoinCompassAPI.Infrastructure.Mappings
{
    public class SavingsGoalMap : IEntityTypeConfiguration<SavingsGoal>
    {
        public void Configure(EntityTypeBuilder<SavingsGoal> builder)
        {
            builder.ToTable("SavingsGoals");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(s => s.UserId)
                .IsRequired();

            builder.Property(s => s.GoalName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.TargetAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.CurrentAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.TargetDate)
                .IsRequired();
        }
    }
}
