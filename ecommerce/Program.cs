using Microsoft.EntityFrameworkCore;
using cms;
using cms.SeedData;
using System.Text.Json.Serialization;
using NJsonSchema.Generation;
using Microsoft.Extensions.Options;
using NJsonSchema;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.IncludeFields = true;
    //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;

    //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    //options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
});
//.AddNewtonsoftJson(options =>
//{
//    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
//    options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
//    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
//});

//})

//builder.Services.AddDbContext<MyDbContext>(options =>
//    options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<MyDbContext>(options =>
            options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//builder.Services.AddSwaggerDocument();



builder.Services.AddOpenApiDocument((options) =>
{

});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOriginPolicy",
        builder =>
        {
            builder.AllowAnyOrigin() // Replace with your client application's URL
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


//builder.Services.AddOpenApiDocument(document =>
//{
//    document.OperationProcessors.Add(new MyOperationProcessor());
//});


var app = builder.Build();

app.UseCors("AllowAnyOriginPolicy");

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
    context.GridRows.RemoveRange(context.GridRows);
    context.GridColumns.RemoveRange(context.GridColumns);
    context.Pages.RemoveRange(context.Pages);
    context.ImageComponents.RemoveRange(context.ImageComponents);
    context.TextComponents.RemoveRange(context.TextComponents);
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
