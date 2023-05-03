﻿namespace cms.ecommerce.Models
{
        public class ProductField
        {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
                public string FieldType { get; set; }
                public bool IsRequired { get; set; }
                public bool IsEnabled { get; set; }

                public string Value { get; set; }
        }

        public class ProductCategory
        {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
                public int? ParentCategoryId { get; set; }
                public ProductCategory ParentCategory { get; set; }
                public ICollection<ProductCategory> Subcategories { get; set; }
        }

        public class Product
        {
                public int Id { get; set; }
                public string Name { get; set; }
                public string Description { get; set; }
                public decimal Price { get; set; }
                public int StockQuantity { get; set; }
                public ProductCategory ProductCategory { get; set; }
                public ICollection<ProductField> ProductFields { get; set; }
        }
}
