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
        [JsonIgnore]
        public virtual Page? ParentPage { get; set; }

        public virtual List<GridRow>? GridRows { get; set; }
    }

    public class GridRow
    {
        public int Id { get; set; } // Primary key
        public virtual List<GridColumn>? Columns { get; set; }

       
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

    [JsonPolymorphic(
        TypeDiscriminatorPropertyName = "$discriminator",
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