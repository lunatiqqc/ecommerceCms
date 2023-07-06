using Microsoft.EntityFrameworkCore;
using cms.ecommerce.Models;
using cms.Models;

namespace cms
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public DbSet<Page> Pages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductField> ProductFields { get; set; }
        public DbSet<GridRow> GridRows { get; set; }
        public DbSet<GridColumn> GridColumns { get; set; }
        public DbSet<TextComponent> TextComponents { get; set; }
        public DbSet<ImageComponent> ImageComponents { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<TextComponent>().ToTable("ComponentBases");
            //modelBuilder.Entity<ImageComponent>().ToTable("ComponentBases");
            // Seed the database
            //ProductSeedData.SeedProducts(modelBuilder);

            //    modelBuilder.Entity<Component>()
            //.ToTable("Components")
            //.HasDiscriminator<string>("Discriminator2")
            //.HasValue<TextComponent>("TextComponent")
            //.HasValue<ImageComponent>("ImageComponent");

            modelBuilder.Entity<TextComponent>()
                .ToTable("TextComponents");

            modelBuilder.Entity<ImageComponent>()
                .ToTable("ImageComponents");
        }

    }


}