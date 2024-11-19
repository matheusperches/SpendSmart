using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) : DbContext(options)
    {
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Code> Codes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Code>()
                .HasIndex(c => c.Value)
                .IsUnique();
        }

    }
}
