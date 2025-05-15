// AppDbContext.cs
// This class defines the application's database context, extending from IdentityDbContext to include identity functionality.
// It configures the relationships between the entities, including composite keys and navigation properties.
// It also includes DbSets for the various entities representing tables in the database.

using HeavenHome.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace HeavenHome.Data
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext>  options) : base(options)
        {   
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material_Product>().HasKey(am => new
            {
                am.MaterialId,
                am.ProductId,
            });

            modelBuilder.Entity<Material_Product>().HasOne(m => m.Product).WithMany(am => am.Materials_Products).HasForeignKey(m  => m.MaterialId);
            modelBuilder.Entity<Material_Product>().HasOne(m => m.Product).WithMany(am => am.Materials_Products).HasForeignKey(m  => m.ProductId);

            base.OnModelCreating(modelBuilder);
        }

        // DbSets representing tables in the database
        public DbSet<Material> Materials { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Material_Product> Materials_Products { get; set; }
        public DbSet<Company> Companies { get; set; }


        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> shoppingCartItems { get; set; }
    }
}
