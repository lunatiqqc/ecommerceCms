using cms.ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace cms.SeedData
{
    public static class ProductSeedData
    {
        public static void SeedProducts(MyDbContext context)
        {
            // Check if products already exist
            if (context.Products.Any())
            {
                return;   // DB has already been seeded
            }

            var categories = new List<ProductCategory>
                {
                new ProductCategory { Name = "Electronics" },
                new ProductCategory { Name = "Home & Garden" },
                new ProductCategory { Name = "Clothing" }
                };

            context.ProductCategories.AddRange(categories);
            context.SaveChanges();

            var fields = new List<ProductField>
                {
                new ProductField { Name = "Brand", FieldType = "Text" },
                new ProductField { Name = "Model", FieldType = "Text" },
                new ProductField { Name = "Color", FieldType = "Text" },
                new ProductField { Name = "Size", FieldType = "Text" }
                };

            context.ProductFields.AddRange(fields);
            context.SaveChanges();

            var products = new List<Product>
                {
                new Product
                {
                        Name = "Samsung Galaxy S21",
                        Price = 999.99m,
                        StockQuantity = 10,
                        ProductCategory = categories.FirstOrDefault(c => c.Name == "Electronics"),
                        ProductFields = new List<ProductField>
                        {
                        new ProductField { Name = "Brand", Value = "Samsung" },
                        new ProductField { Name = "Model", Value = "Galaxy S21" },
                        new ProductField { Name = "Color", Value = "Phantom Black" },
                        new ProductField { Name = "Size", Value = "6.2 inches" }
                        }
                },
                new Product
                {
                        Name = "Wooden Dining Table",
                        Description = "Large wooden dining table for the whole family",
                        Price = 799.99m,
                        StockQuantity = 5,
                        ProductCategory = categories.FirstOrDefault(c => c.Name == "Home & Garden"),
                        ProductFields = new List<ProductField>
                        {
                        new ProductField { Name = "Brand", Value = "Furniture World" },
                        new ProductField { Name = "Color", Value = "Brown" },
                        new ProductField { Name = "Size", Value = "96 x 40 inches" }
                        }
                },
                new Product
                {
                        Name = "Levi's 501 Jeans",
                        Description = "Classic fit jeans for men",
                        Price = 69.99m,
                        StockQuantity = 20,
                        ProductCategory = categories.FirstOrDefault(c => c.Name == "Clothing"),
                        ProductFields = new List<ProductField>
                        {
                        new ProductField { Name = "Brand", Value = "Levi's" },
                        new ProductField { Name = "Color", Value = "Blue" },
                        new ProductField { Name = "Size", Value = "32W x 34L" }
                        }
                }
                };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
