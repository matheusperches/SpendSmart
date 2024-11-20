using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) : DbContext(options)
    {
        public required DbSet<Expense> Expenses { get; set; }
        public required DbSet<Code> Codes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Code>()
                .HasIndex(c => c.Value)
                .IsUnique();
        }
    }
}
