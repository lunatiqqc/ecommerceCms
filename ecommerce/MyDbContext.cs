using Microsoft.EntityFrameworkCore;
using cms.ecommerce.Data;
using cms.ecommerce.Models;

namespace cms
{
        public class MyDbContext : DbContext
        {
                public MyDbContext(DbContextOptions<MyDbContext> options)
                    : base(options)
                {
                }

                public DbSet<Product> Products { get; set; }
                public DbSet<ProductCategory> ProductCategories { get; set; }
                public DbSet<ProductField> ProductFields { get; set; }

                protected override void OnModelCreating(ModelBuilder modelBuilder)
                {
                        base.OnModelCreating(modelBuilder);

                        // Configure your entities here...

                        // Seed your database
                        ProductSeedData.SeedProducts(this);
                }
        }

}
