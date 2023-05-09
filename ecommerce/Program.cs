using Microsoft.EntityFrameworkCore;
using cms;
using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;
using cms.SeedData;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseLazyLoadingProxies().
    UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddSwaggerDocument();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();


app.UseOpenApi();
app.UseSwaggerUi3();
app.UseCors();


using (var context = new MyDbContext(new DbContextOptionsBuilder<MyDbContext>().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).Options))
{

    context.Database.EnsureCreated();

    // Remove all existing pages
    context.Pages.RemoveRange(context.Pages);
    context.SaveChanges();

    PageSeedData.SeedPages(context);

    // Remove all the products, product fields, and product categories
    context.Products.RemoveRange(context.Products);
    context.ProductFields.RemoveRange(context.ProductFields);
    context.ProductCategories.RemoveRange(context.ProductCategories);
    context.SaveChanges();

    ProductSeedData.SeedProducts(context);

}

app.UseAuthorization();

app.MapControllers();

app.Run();
