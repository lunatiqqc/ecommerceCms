using Microsoft.EntityFrameworkCore;
using cms;
using cms.SeedData;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

//builder.Services.AddDbContext<MyDbContext>(options =>
//    options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddSwaggerDocument();

builder.Services.AddSwaggerDocument();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5173") // Replace with your client application's URL
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


//builder.Services.AddOpenApiDocument(document =>
//{
//    document.OperationProcessors.Add(new MyOperationProcessor());
//});


var app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.UseOpenApi();
app.UseSwaggerUi3();

//app.UseSwagger();

// This middleware serves the Swagger documentation UI
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
//});


using (var context = new MyDbContext(new DbContextOptionsBuilder<MyDbContext>().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).Options))
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