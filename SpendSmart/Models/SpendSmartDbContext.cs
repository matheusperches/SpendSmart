using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) : DbContext(options)
    {
        public DbSet<Expense> Expenses { get; set; } = null!;
        public DbSet<Code> Codes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Code>()
                .HasIndex(c => c.ShortCode)
                .IsUnique();
            modelBuilder.Entity<Expense>()
                .HasOne(e => e.Code)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CodeId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: Delete all expenses if a code is deleted
        }
    }
}
