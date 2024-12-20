using CoinCompassAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoinCompassAPI.Infrastructure.Persistence
{
    public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }

        public DbSet<Account> Contas { get; set; }
        public DbSet<Transaction> Transacoes { get; set; }
        public DbSet<Investment> Investimentos { get; set; }
        public DbSet<SavingsGoal> MetaEconomias { get; set; }
        public DbSet<Budget> Orcamentos { get; set; }
        public DbSet<Outgoings> Gastos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Mappings.AccountMap());
            modelBuilder.ApplyConfiguration(new Mappings.BudgetMap());
            modelBuilder.ApplyConfiguration(new Mappings.InvestmentMap());
            modelBuilder.ApplyConfiguration(new Mappings.OutgoingsMap());
            modelBuilder.ApplyConfiguration(new Mappings.SavingsGoalMap());
            modelBuilder.ApplyConfiguration(new Mappings.TransactionMap());
            modelBuilder.ApplyConfiguration(new Mappings.ApplicationUserMap());
        }
    }
}
