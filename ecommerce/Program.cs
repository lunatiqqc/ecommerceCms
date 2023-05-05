using Microsoft.EntityFrameworkCore;
using cms;
using System.Reflection.Metadata;
using Microsoft.Extensions.Configuration;
using cms.ecommerce.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseLazyLoadingProxies().
    UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddSwaggerDocument();

var app = builder.Build();


app.UseOpenApi();
app.UseSwaggerUi3();


using (var context = new MyDbContext(new DbContextOptionsBuilder<MyDbContext>().UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")).Options))
{

    context.Database.EnsureCreated();

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
