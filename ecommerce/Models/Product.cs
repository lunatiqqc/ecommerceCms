using System.Text.Json.Serialization;
namespace cms.ecommerce.Models
{
    public class ProductField
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? FieldType { get; set; }
        public bool? IsEnabled { get; set; }
        public string? Value { get; set; }
    }

    public class ProductCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public virtual ProductCategory? ParentCategory { get; set; }
        public virtual IEnumerable<ProductCategory>? Subcategories { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public virtual ProductCategory? ProductCategory { get; set; }
        public virtual IEnumerable<ProductField>? ProductFields { get; set; }
    }
}
