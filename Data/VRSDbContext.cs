using Microsoft.EntityFrameworkCore;
using VRSAPI.Models;

namespace VRSAPI.Data
{
    public class VRSDbContext : DbContext
    {
        public VRSDbContext(DbContextOptions<VRSDbContext> options) : base(options)
        {
        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuItemImage> MenuItemImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Additional configurations can be added here if needed
        }

    }
}