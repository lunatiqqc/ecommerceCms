using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using cms.models;
using cms.models.styling;
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
using cms.users;

namespace cms.models
{
    public class Page
    {
	public int Id { get; set; }
	public string? Title { get; set; }
	public string? Url { get; set; }
	public virtual List<Page>? Children { get; set; }
	public bool? VisibleInMenu { get; set; }
	public UserRoles? RequiredRole { get; set; }
	[JsonIgnore]
	public virtual Page? ParentPage { get; set; }

	public virtual GridContent? GridContent { get; set; }

	public int? GridContentId { get; set; }


    }

    public class GridContent
    {
	public int Id { get; set; }
	public virtual List<GridRow>? GridRows { get; set; }
    }


    public class GridColumn
    {
	public int Id { get; set; }

	private int _width;
	public int ColumnStart { get; set; } = 0;

	public int Width
	{
	    get => _width;
	    set => _width = (value >= 1 && value <= 12) ? value : 1;
	}


	public virtual Component? Component { get; set; } // Reference to the component
	public virtual GridContent? GridContent { get; set; }
	public int? GridContentId { get; set; }

	public int? GridRowId { get; set; }

	public virtual GridRow? GridRow { get; set; }

	public virtual ContainerStyling? Styling { get; set; }
    }


    public class GridRow
    {
	public int Id { get; set; }

	public virtual List<GridColumn>? Columns { get; set; }
	public virtual ContainerStyling? Styling { get; set; }
    }

    [JsonPolymorphic(
	TypeDiscriminatorPropertyName = "discriminator",
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToNearestAncestor)]

    [JsonDerivedType(typeof(TextComponent), typeDiscriminator: "TextComponent")]
    [JsonDerivedType(typeof(ImageComponent), typeDiscriminator: "ImageComponent")]
    [JsonDerivedType(typeof(MenuComponent), typeDiscriminator: "MenuComponent")]
    public abstract class Component
    {
	public int Id { get; set; }
	public virtual ContainerStyling? Styling { get; set; }

	// Common properties for all components
	// ...
    }

    public class TextFormats
    {
	public int Id { get; set; }
	[Column(TypeName = "text")]
	public BoldClass? Bold { get; set; }
	[Column(TypeName = "text")]
	public ItalicClass? Italic { get; set; }
	[Column(TypeName = "text")]
	public UnderlineClass? Underline { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum BoldClass
    {
	[EnumMember(Value = "font-bold")]
	bold
    }
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ItalicClass
    {
	[EnumMember(Value = "italic")]
	italic
    }
    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum UnderlineClass
    {
	[EnumMember(Value = "underline")]
	underline
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]
    public enum ElementFormats
    {
	[EnumMember(Value = "text-left")]
	align_left,
	[EnumMember(Value = "text-center")]
	align_center,
	[EnumMember(Value = "text-right")]
	align_right,
	[EnumMember(Value = "text-justify")]
	align_justify
    }

    [JsonConverter(typeof(JsonStringEnumMemberConverter))]

    public enum FontSizeClassOptions
    {
	[EnumMember(Value = "text-4xl")]
	Font4xl,

	[EnumMember(Value = "text-3xl")]
	Font3xl,

	[EnumMember(Value = "text-2xl")]
	Font2xl,

	[EnumMember(Value = "text-xl")]
	FontXLarge,

	[EnumMember(Value = "text-lg")]
	FontLarge,

	[EnumMember(Value = "text-base")]
	FontStandard
    }

    // Extended ContainerStyling for TextComponent
    public class TextContainerStyling
    {
	public int Id { get; set; }
	// Additional properties specific to text components' styling
	// ...

	public virtual TextFormats? TextFormats { get; set; }
	public ElementFormats? ElementFormats { get; set; }

	public FontSizeClassOptions? FontSizeClassOptions { get; set; }

    }

    public class TextComponent : Component
    {
	public string? Html { get; set; }
	public string? EditorState { get; set; }

	public virtual TextContainerStyling? TextStyling { get; set; } // Use 'new' keyword to hide the base class property


	// Additional properties specific to text components
	// ...
    }

    public class ImageComponent : Component
    {
	public string? ImageUrl { get; set; }
	// Additional properties specific to image components
	// ...
    }
    public class MenuComponent : Component
    {
	//public IEnumerable<Page> Items { get; set; }
	// public string? ImageUrl { get; set; }
	// Additional properties specific to image components
	// ...
	public virtual TextContainerStyling? TextStyling { get; set; } // Use 'new' keyword to hide the base class property

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