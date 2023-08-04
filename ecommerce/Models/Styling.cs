using Castle.Components.DictionaryAdapter;
using cms.models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace cms.models.styling
{
    public class ContainerStyling
    {
	public int Id { get; set; }
	public virtual Padding? Padding { get; set; }
	public virtual Margin? Margin { get; set; }

	public virtual Background? Background { get; set; }

	public float? Height { get; set; }

    }

    public class Padding
    {
	public int Id { get; set; }

	public int? Top { get; set; }
	public int? Right { get; set; }
	public int? Bottom { get; set; }
	public int? Left { get; set; }
    }
    public class Margin
    {
	public int Id { get; set; }

	public int? Top { get; set; }
	public int? Right { get; set; }
	public int? Bottom { get; set; }
	public int? Left { get; set; }
    }

    public class Background
    {
	public int Id { get; set; }
	
	public virtual ImageStyle? BackgroundImage { get; set; }

    }

    public class ImageStyle
    {
	public int Id { get; set; }
	public virtual ImageFile? ImageFile { get; set; }

	[JsonConverter(typeof(JsonStringEnumMemberConverter))]
	public enum ObjectFitOption
	{
	    [EnumMember(Value = "object-cover")]
	    object_cover,
	    [EnumMember(Value = "object-contain")]
	    object_contain
	}

	[Column(TypeName = "text")] // Specify the desired column data type
	public ObjectFitOption? ObjectFit { get; set; }


    }
}
