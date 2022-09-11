using Microsoft.EntityFrameworkCore;

namespace CarValuer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CarsMaintenanceCostsPerYear> CarsMaintenanceCostsPerYear { get; set; }

        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
