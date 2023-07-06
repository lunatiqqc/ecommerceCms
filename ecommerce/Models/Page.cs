using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using cms.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Reflection;
//using NJsonSchema.Converters;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Rewrite;
using System.Drawing;
using Swashbuckle.AspNetCore.Annotations;
using OneOf;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace cms.Models
{
    public class Page
    {
	public int? Id { get; set; }
	public string? Title { get; set; }
	public string? Url { get; set; }
	public virtual List<Page>? Children { get; set; }
	public bool? VisibleInMenu { get; set; }
	public Users.UserRoles? RequiredRole { get; set; }
	[System.Text.Json.Serialization.JsonIgnore]
	public virtual Page? ParentPage { get; set; }

	public virtual List<GridRow>? GridRows { get; set; }
    }


    public class GridColumn
    {
	public int Id { get; set; } // Primary key
	private int _width;
	public int ColumnStart { get; set; } = 0;

	public int Width
	{
	    get => _width;
	    set => _width = (value >= 1 && value <= 12) ? value : 1;
	}

	
	public virtual Component? Component { get; set; } // Reference to the component
	public virtual List<GridRow>? GridRows { get; set; }
    }


    public class GridRow
    {
	public int Id { get; set; } // Primary key
	public virtual List<GridColumn>? Columns { get; set; }

    }

    [JsonPolymorphic(
	TypeDiscriminatorPropertyName = "discriminator",
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]

    [JsonDerivedType(typeof(TextComponent), typeDiscriminator: "TextComponent")]
    [JsonDerivedType(typeof(ImageComponent), typeDiscriminator: "ImageComponent")]
    public abstract class Component
    {
	public int Id { get; set; }
	// Common properties for all components
	// ...
    }



    public class TextComponent : Component
    {
	public string? Text { get; set; }
	// Additional properties specific to text components
	// ...
    }

    public class ImageComponent : Component
    {
	public string? ImageUrl { get; set; }
	// Additional properties specific to image components
	// ...
    }
}


//public class ComponentConverter : Newtonsoft.Json.JsonConverter
//{
//    public override bool CanConvert(Type objectType)
//    {
//	return typeof(Component).IsAssignableFrom(objectType);
//    }
//
//    public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
//    {
//	JToken t = JToken.FromObject(value);
//	if (t.Type != JTokenType.Object)
//	{
//	    t.WriteTo(writer);
//	    return;
//	}
//
//	JObject o = (JObject)t;
//	o.AddFirst(new JProperty("$type", value.GetType().FullName));
//	o.WriteTo(writer);
//    }
//
//    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
//    {
//	throw new NotImplementedException();
//    }
//}

//public class ComponentConverter : Newtonsoft.Json.JsonConverter<Component>
//{
//    public override bool CanWrite => false;
//
//    public override Component ReadJson(JsonReader reader, Type objectType, Component existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
//    {
//	JObject jo = JObject.Load(reader);
//	if (jo.ContainsKey("Text"))
//	    return jo.ToObject<TextComponent>(serializer);
//	if (jo.ContainsKey("ImageUrl"))
//	    return jo.ToObject<ImageComponent>(serializer);
//	return null;
//    }
//
//    public override void WriteJson(JsonWriter writer, Component value, Newtonsoft.Json.JsonSerializer serializer)
//    {
//	throw new NotImplementedException();
//    }
//}