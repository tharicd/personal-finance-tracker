using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.API.Models;

namespace PersonalFinanceTracker.API
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
        public DbSet<FinanceEntry> FinanceEntries { get; set; }
    }
}
