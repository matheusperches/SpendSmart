using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) : DbContext(options)
    {
        public DbSet<Expense> Expenses { get; set; }
    }
}
