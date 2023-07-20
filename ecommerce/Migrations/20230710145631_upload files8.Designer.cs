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
    [Migration("20230710145631_upload files8")]
    partial class uploadfiles8
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

            modelBuilder.Entity("cms.Models.CmsFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("FileFolderId")
                        .HasColumnType("integer");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("FileFolderId");

                    b.ToTable("CmsFiles", (string)null);

                    b.UseTptMappingStrategy();
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

            modelBuilder.Entity("cms.Models.FileFolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.ToTable("FileFolders");
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

                    b.Property<int?>("GridContentId")
                        .HasColumnType("integer");

                    b.Property<int?>("GridRowId")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("GridContentId");

                    b.HasIndex("GridRowId");

                    b.ToTable("GridColumns");
                });

            modelBuilder.Entity("cms.Models.GridContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("GridContent");
                });

            modelBuilder.Entity("cms.Models.GridRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GridContentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GridContentId");

                    b.ToTable("GridRows");
                });

            modelBuilder.Entity("cms.Models.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GridContentId")
                        .HasColumnType("integer");

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

                    b.HasIndex("GridContentId");

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

            modelBuilder.Entity("cms.Models.ImageFile", b =>
                {
                    b.HasBaseType("cms.Models.CmsFile");

                    b.Property<float>("AspectRatio")
                        .HasColumnType("real");

                    b.Property<int[]>("Sizes")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.ToTable("ImageFiles", (string)null);
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

            modelBuilder.Entity("cms.Models.CmsFile", b =>
                {
                    b.HasOne("cms.Models.FileFolder", "FileFolder")
                        .WithMany("Files")
                        .HasForeignKey("FileFolderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FileFolder");
                });

            modelBuilder.Entity("cms.Models.FileFolder", b =>
                {
                    b.HasOne("cms.Models.FileFolder", "ParentCategory")
                        .WithMany("Subcategories")
                        .HasForeignKey("ParentCategoryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("cms.Models.GridColumn", b =>
                {
                    b.HasOne("cms.Models.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId");

                    b.HasOne("cms.Models.GridContent", "GridContent")
                        .WithMany()
                        .HasForeignKey("GridContentId");

                    b.HasOne("cms.Models.GridRow", "GridRow")
                        .WithMany("Columns")
                        .HasForeignKey("GridRowId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Component");

                    b.Navigation("GridContent");

                    b.Navigation("GridRow");
                });

            modelBuilder.Entity("cms.Models.GridRow", b =>
                {
                    b.HasOne("cms.Models.GridContent", null)
                        .WithMany("GridRows")
                        .HasForeignKey("GridContentId");
                });

            modelBuilder.Entity("cms.Models.Page", b =>
                {
                    b.HasOne("cms.Models.GridContent", "GridContent")
                        .WithMany()
                        .HasForeignKey("GridContentId");

                    b.HasOne("cms.Models.Page", "ParentPage")
                        .WithMany("Children")
                        .HasForeignKey("ParentPageId");

                    b.Navigation("GridContent");

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

            modelBuilder.Entity("cms.Models.ImageFile", b =>
                {
                    b.HasOne("cms.Models.CmsFile", null)
                        .WithOne()
                        .HasForeignKey("cms.Models.ImageFile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("cms.Models.FileFolder", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("cms.Models.GridContent", b =>
                {
                    b.Navigation("GridRows");
                });

            modelBuilder.Entity("cms.Models.GridRow", b =>
                {
                    b.Navigation("Columns");
                });

            modelBuilder.Entity("cms.Models.Page", b =>
                {
                    b.Navigation("Children");
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
