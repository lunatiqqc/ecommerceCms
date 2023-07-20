using Microsoft.EntityFrameworkCore;
using cms;
using cms.SeedData;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Hosting;
using cms.models;
using Microsoft.EntityFrameworkCore.Design;
using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    Args = args,
    WebRootPath = "wwwroot",


});




// Add services to the container.

builder.Services.AddAutoMapper(typeof(MappingProfile) /*, ...*/);

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    //options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    // options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    //options.JsonSerializerOptions.IncludeFields = true;
    //options.JsonSerializerOptions.Converters.Add(new CustomGridRowListConverter());
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
{
    options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(true);
});


//builder.Services.AddSwaggerDocument();

builder.Services.AddSwaggerGen((options) =>
{
    //options.UseInlineDefinitionsForEnums();
    //options.UseAllOfForInheritance();
    //
    //options.SelectSubTypesUsing(baseType =>
    //    typeof(Program).Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType))
    //);

    //options.SelectDiscriminatorNameUsing((baseType) => "TypeName");
    //options.SelectDiscriminatorValueUsing((subType) => subType.Name + "test-subtype-name");

    //options.UseAllOfForInheritance();

    //options.SelectSubTypesUsing(baseType =>
    //{
    //    return typeof(Startup).Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType));
    //})

    options.UseOneOfForPolymorphism();
    options.SelectDiscriminatorNameUsing((baseType) => "discriminator");

    options.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
    options.SupportNonNullableReferenceTypes(); // Sets Nullable flags appropriately.              
    options.UseAllOfToExtendReferenceSchemas(); // Allows $ref enums to be nullable
    options.UseAllOfForInheritance();  // Allows $ref objects to be nullable
				 //options.SelectDiscriminatorValueUsing((subType) => subType.Name);
				 //options.SelectSubTypesUsing(baseType =>
				 //{
				 //    if (baseType == typeof(Component))
				 //    {
				 //        return new[]
				 //        {
				 //            typeof(TextComponent),
				 //            typeof(ImageComponent)
				 //        };
				 //    }
				 //    return Enumerable.Empty<Type>();
				 //});

    //options.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

    options.CustomOperationIds(apiDesc =>
   {
       return apiDesc.TryGetMethodInfo(out MethodInfo methodInfo) ? (apiDesc.ActionDescriptor.RouteValues["controller"] + "_" + methodInfo.Name) : null;
   });

    options.AddServer(new OpenApiServer { Url = "http://localhost:5059" }); // Set the desired base URL


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
app.UseSwagger();
app.UseSwaggerUI();


//app.UseSwagger();

// This middleware serves the Swagger documentation UI
//app.UseSwaggerUI(c =>
//{
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
//});

string[] commandLineArgs = Environment.GetCommandLineArgs();

bool isExecutingByEfCore = args.Length > 0 && args[0] == "--ef-core";



//using (var context = new MyDbContext(new DbContextOptionsBuilder<MyDbContext>().UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).Options))
//{
//
//    context.Database.EnsureCreated();
//
//    context.ImageComponents.RemoveRange(context.ImageComponents);
//    context.TextComponents.RemoveRange(context.TextComponents);
//    context.GridColumns.RemoveRange(context.GridColumns);
//
//    // Remove all existing grid rows
//    context.GridRows.RemoveRange(context.GridRows);
//
//    foreach (var gridRow in context.GridRows)
//    {
//	if (gridRow.Columns != null)
//	{
//	    gridRow.Columns.Clear();
//	}
//    }
//
//    // Remove all existing pages
//    context.Pages.RemoveRange(context.Pages);
//
//    context.GridContent.RemoveRange(context.GridContent);
//
//    // Remove other related entities
//
//    context.SaveChanges();
//
//    PageSeedData.SeedPages(context);
//
//    // Remove all the products, product fields, and product categories
//    context.Products.RemoveRange(context.Products);
//    context.ProductFields.RemoveRange(context.ProductFields);
//    context.ProductCategories.RemoveRange(context.ProductCategories);
//    context.SaveChanges();
//
//    ProductSeedData.SeedProducts(context);
//
//}

app.UseStaticFiles(); // Enables serving static files from the "wwwroot" folder

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace ecommerce
{
    public class MyDbContextFactory : IDesignTimeDbContextFactory<MyDbContext>
    {
	public MyDbContext CreateDbContext(string[] args)
	{
	    IConfigurationRoot configuration = new ConfigurationBuilder()
		.SetBasePath(Directory.GetCurrentDirectory())
		.AddJsonFile("appsettings.json")
		.Build();

	    var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
	    optionsBuilder.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

	    return new MyDbContext(optionsBuilder.Options);
	}
    }
}

public class MappingProfile : Profile
{
    public MappingProfile()
    {
	//reateMap<Page, Page>()
	//       .ForMember(dest => dest.Children, opt => opt.Ignore())
	//   .ForMember(dest => dest.ParentPage, opt => opt.Ignore())
	//   .ForMember(dest => dest.GridRows, opt => opt.MapFrom(src => src.GridRows));


	//CreateMap<Page, Page>().ForMember(dest => dest.Children, opt => opt.Ignore())
	//   .ForMember(dest => dest.ParentPage, opt => opt.Ignore())
	//   .ForMember(dest => dest.GridRows, opt => opt.MapFrom(src => src.GridRows));
	//CreateMap<GridRow, GridRow>();
	//CreateMap<GridColumn, GridColumn>();
	//
	//// Map Component and its derived types
	//CreateMap<Component, Component>()
	//    .Include<TextComponent, TextComponent>()
	//    .Include<ImageComponent, ImageComponent>();
	//
	//CreateMap<TextComponent, TextComponent>();
	//CreateMap<ImageComponent, ImageComponent>();

	//CreateMap<GridRow, GridRow>()
	//    .ForMember(dest => dest.Columns, opt => opt.MapFrom(src => src.Columns));
	//
	//CreateMap<GridColumn, GridColumn>()
	//    .ForMember(dest => dest.Component, opt => opt.MapFrom(src => src.Component));
	//.ForMember(dest => dest.GridRows, opt => opt.MapFrom(src => src.GridRows));

	//CreateMap<Component, Component>()
	//    .Include<TextComponent, TextComponent>()
	//    .Include<ImageComponent, ImageComponent>();
	//
	//CreateMap<TextComponent, TextComponent>();
	//CreateMap<ImageComponent, ImageComponent>();
    }
}

public class CustomGridRowListConverter : JsonConverter<List<GridRow>>
{
    public override List<GridRow> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
	// Implement deserialization logic here
	throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, List<GridRow> value, JsonSerializerOptions options)
    {
	writer.WriteStartArray();
	foreach (var item in value)
	{
	    if (item != null)
	    {
		var newOptions = new JsonSerializerOptions(options);
		newOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
		JsonSerializer.Serialize(writer, item, newOptions);
	    }
	}
	writer.WriteEndArray();
    }
}

public class RequireNonNullablePropertiesSchemaFilter : ISchemaFilter
{
    /// <summary>
    /// Add to model.Required all properties where Nullable is false.
    /// </summary>
    public void Apply(OpenApiSchema model, SchemaFilterContext context)
    {
	var additionalRequiredProps = model.Properties
	    .Where(x => !x.Value.Nullable && !model.Required.Contains(x.Key))
	    .Select(x => x.Key);
	foreach (var propKey in additionalRequiredProps)
	{
	    model.Required.Add(propKey);
	}
    }
}