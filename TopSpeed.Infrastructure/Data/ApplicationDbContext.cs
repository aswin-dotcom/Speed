

using Microsoft.EntityFrameworkCore;
using TopSpeed.Domain.Models;

namespace TopSpeed.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext

    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
        
        {
                
        }
        public DbSet<Brand> Brand { get; set; } 
        public DbSet<VehicleType> VehicleTypes { get; set; }
    }
}
