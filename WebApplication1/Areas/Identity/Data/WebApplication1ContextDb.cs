using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class WebApplication1ContextDb : IdentityDbContext<IdentityUser>
{
    public WebApplication1ContextDb(DbContextOptions<WebApplication1ContextDb> options)
        : base(options)
    {
    }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
