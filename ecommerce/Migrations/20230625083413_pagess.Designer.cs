﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using cms;

#nullable disable

namespace ecommerce.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230625083413_pagess")]
    partial class pagess
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GridColumnGridRow", b =>
                {
                    b.Property<int>("ColumnsId")
                        .HasColumnType("integer");

                    b.Property<int>("GridRowsId")
                        .HasColumnType("integer");

                    b.HasKey("ColumnsId", "GridRowsId");

                    b.HasIndex("GridRowsId");

                    b.ToTable("GridColumnGridRow");
                });

            modelBuilder.Entity("cms.Models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Component");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("cms.Models.GridColumn", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ColumnStart")
                        .HasColumnType("integer");

                    b.Property<int?>("ComponentId")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.ToTable("GridColumns");
                });

            modelBuilder.Entity("cms.Models.GridRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("PageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.ToTable("GridRows");
                });

            modelBuilder.Entity("cms.Models.Page", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<int?>("ParentPageId")
                        .HasColumnType("integer");

                    b.Property<int?>("RequiredRole")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<bool?>("VisibleInMenu")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("ParentPageId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("cms.ecommerce.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal?>("Price")
                        .HasColumnType("numeric");

                    b.Property<int?>("ProductCategoryId")
                        .HasColumnType("integer");

                    b.Property<int?>("StockQuantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProductCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("cms.ecommerce.Models.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("cms.ecommerce.Models.ProductField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("FieldType")
                        .HasColumnType("text");

                    b.Property<bool?>("IsEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int?>("ProductId")
                        .HasColumnType("integer");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductFields");
                });

            modelBuilder.Entity("cms.Models.ImageComponent", b =>
                {
                    b.HasBaseType("cms.Models.Component");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.ToTable("ImageComponents", (string)null);
                });

            modelBuilder.Entity("cms.Models.TextComponent", b =>
                {
                    b.HasBaseType("cms.Models.Component");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.ToTable("TextComponents", (string)null);
                });

            modelBuilder.Entity("GridColumnGridRow", b =>
                {
                    b.HasOne("cms.Models.GridColumn", null)
                        .WithMany()
                        .HasForeignKey("ColumnsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("cms.Models.GridRow", null)
                        .WithMany()
                        .HasForeignKey("GridRowsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cms.Models.GridColumn", b =>
                {
                    b.HasOne("cms.Models.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId");

                    b.Navigation("Component");
                });

            modelBuilder.Entity("cms.Models.GridRow", b =>
                {
                    b.HasOne("cms.Models.Page", null)
                        .WithMany("GridRows")
                        .HasForeignKey("PageId");
                });

            modelBuilder.Entity("cms.Models.Page", b =>
                {
                    b.HasOne("cms.Models.Page", "ParentPage")
                        .WithMany("Children")
                        .HasForeignKey("ParentPageId");

                    b.Navigation("ParentPage");
                });

            modelBuilder.Entity("cms.ecommerce.Models.Product", b =>
                {
                    b.HasOne("cms.ecommerce.Models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryId");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("cms.ecommerce.Models.ProductCategory", b =>
                {
                    b.HasOne("cms.ecommerce.Models.ProductCategory", "ParentCategory")
                        .WithMany("Subcategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("cms.ecommerce.Models.ProductField", b =>
                {
                    b.HasOne("cms.ecommerce.Models.Product", null)
                        .WithMany("ProductFields")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("cms.Models.ImageComponent", b =>
                {
                    b.HasOne("cms.Models.Component", null)
                        .WithOne()
                        .HasForeignKey("cms.Models.ImageComponent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cms.Models.TextComponent", b =>
                {
                    b.HasOne("cms.Models.Component", null)
                        .WithOne()
                        .HasForeignKey("cms.Models.TextComponent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cms.Models.Page", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("GridRows");
                });

            modelBuilder.Entity("cms.ecommerce.Models.Product", b =>
                {
                    b.Navigation("ProductFields");
                });

            modelBuilder.Entity("cms.ecommerce.Models.ProductCategory", b =>
                {
                    b.Navigation("Subcategories");
                });
#pragma warning restore 612, 618
        }
    }
}
