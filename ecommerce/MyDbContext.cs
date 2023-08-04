using Microsoft.EntityFrameworkCore;
using cms.ecommerce.models;
using cms.models;
using cms.models.styling;

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
	public DbSet<GridContent> GridContent { get; set; }
	public DbSet<GridRow> GridRows { get; set; }
	public DbSet<GridColumn> GridColumns { get; set; }
	public DbSet<TextComponent> TextComponents { get; set; }
	public DbSet<ImageComponent> ImageComponents { get; set; }
	public DbSet<MenuComponent> MenuComponents { get; set; }
	public DbSet<TextFormats> TextFormats { get; set; }

	public DbSet<CmsFile> Files { get; set; }
	public DbSet<ImageFile> ImageFiles { get; set; }
	public DbSet<FileFolder> FileFolders { get; set; }
	public DbSet<GlobalHeader> GlobalHeaders { get; set; }
	public DbSet<GlobalFooter> GlobalFooters { get; set; }
	//public DbSet<BaseStyling> BaseStyling { get; set; }



	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
	    base.OnModelCreating(modelBuilder);
	    modelBuilder.Entity<Page>().HasData(
	    new Page
	    {
	       Id = 1,
	       Title = "Home",
	       Url = "/",
	       VisibleInMenu = true,
	       // Set other properties as needed
	    });
	    //modelBuilder.Entity<TextComponent>().ToTable("ComponentBases");
	    //modelBuilder.Entity<ImageComponent>().ToTable("ComponentBases");
	    // Seed the database
	    //ProductSeedData.SeedProducts(modelBuilder);

	    //    modelBuilder.Entity<Component>()
	    //.ToTable("Components")
	    //.HasDiscriminator<string>("Discriminator2")
	    //.HasValue<TextComponent>("TextComponent")
	    //.HasValue<ImageComponent>("ImageComponent");

	    //modelBuilder.Entity<GridColumn>()
	    //.HasMany(g => g.GridRows)
	    //.WithMany(g => g.Columns)
	    //.UsingEntity<Dictionary<string, object>>(
	    //    "GridColumnGridRow",
	    //    j => j.HasOne<GridRow>().WithMany().HasForeignKey("GridRowId").IsRequired(false),
	    //    j => j.HasOne<GridColumn>().WithMany().HasForeignKey("GridColumnId").IsRequired(false)
	    //);
	    //modelBuilder.Entity<GridContent>()
	    //.HasMany(gc => gc.GridRows)
	    //.WithOne(gr => gr.GridContent)
	    //.OnDelete(DeleteBehavior.Cascade);

	    modelBuilder.Entity<GridRow>()
		.ToTable("GridRows")
		.HasMany(gr => gr.Columns)
		.WithOne((gc) => gc.GridRow)
		.HasForeignKey((gc) => gc.GridRowId)
		.OnDelete(DeleteBehavior.Cascade);

	    //modelBuilder.Entity<BaseStyling>()
	    //.ToTable("BaseStyling") // Specify the table name
	    //.HasDiscriminator<string>("ContentContainer") // Discriminator column
	    //.HasValue<GridRow>("GridRow") // Discriminator value for GridColumn
	    //.HasValue<GridColumn>("GridColumn"); // Discriminator value for GridColumn


	    modelBuilder.Entity<GridColumn>()
		.ToTable("GridColumns");


	    modelBuilder.Entity<TextComponent>()
		.ToTable("TextComponents");

	    modelBuilder.Entity<ImageComponent>()
		.ToTable("ImageComponents");


	    modelBuilder.Entity<CmsFile>().ToTable("CmsFiles");
	    modelBuilder.Entity<ImageFile>().ToTable("ImageFiles");

	    modelBuilder.Entity<CmsFile>()
	      .HasOne(f => f.FileFolder)
	      .WithMany(c => c.Files)
	      .HasForeignKey(f => f.FileFolderId)
	      .OnDelete(DeleteBehavior.Restrict);

	    modelBuilder.Entity<FileFolder>()
		.HasOne(c => c.ParentFolder)
		.WithMany(c => c.Subfolders)
		.HasForeignKey(c => c.ParentFolderId)
		.OnDelete(DeleteBehavior.Restrict);


	}

    }


}