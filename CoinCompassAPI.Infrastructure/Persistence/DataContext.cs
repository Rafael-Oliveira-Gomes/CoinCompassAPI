using System.Reflection;
using CoinCompassAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CoinCompassAPI.Infrastructure.Persistence
{
    public class DataContext : IdentityDbContext<User, ApplicationRole, string>
    {
        public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }

        public DbSet<Account> Contas { get; set; }
        public DbSet<Transaction> Transacoes { get; set; }
        public DbSet<Investment> Investimentos { get; set; }
        public DbSet<SavingsGoal> MetaEconomias { get; set; }
        public DbSet<Budget> Orcamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Entity<User>().ToTable("AspNetUsers").HasKey(t => t.Id);

            base.OnModelCreating(builder);
        }
    }
}
