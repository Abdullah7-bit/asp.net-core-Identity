using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {          
        }

        public DbSet<Sales> Sales { get; set; }
        /*
         * This here is the code that tracks your model for DB.
         * public DbSet<model_name here> model_name_here {get; set;}
         */
    }
}
