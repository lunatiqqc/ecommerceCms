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
    [Migration("20230730220635_text editor")]
    partial class texteditor
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

            modelBuilder.Entity("cms.ecommerce.models.Product", b =>
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

            modelBuilder.Entity("cms.ecommerce.models.ProductCategory", b =>
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

            modelBuilder.Entity("cms.ecommerce.models.ProductField", b =>
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

            modelBuilder.Entity("cms.models.CmsFile", b =>
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

            modelBuilder.Entity("cms.models.Component", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Component");

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("cms.models.FileFolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ParentFolderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ParentFolderId");

                    b.ToTable("FileFolders");
                });

            modelBuilder.Entity("cms.models.GlobalFooter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GridContentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GridContentId");

                    b.ToTable("GlobalFooters");
                });

            modelBuilder.Entity("cms.models.GlobalHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GridContentId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GridContentId");

                    b.ToTable("GlobalHeaders");
                });

            modelBuilder.Entity("cms.models.GridColumn", b =>
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

                    b.Property<int?>("StylingId")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ComponentId");

                    b.HasIndex("GridContentId");

                    b.HasIndex("GridRowId");

                    b.HasIndex("StylingId");

                    b.ToTable("GridColumns", (string)null);
                });

            modelBuilder.Entity("cms.models.GridContent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("GridContent");
                });

            modelBuilder.Entity("cms.models.GridRow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("GridContentId")
                        .HasColumnType("integer");

                    b.Property<int?>("StylingId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GridContentId");

                    b.HasIndex("StylingId");

                    b.ToTable("GridRows", (string)null);
                });

            modelBuilder.Entity("cms.models.Page", b =>
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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "Home",
                            Url = "/",
                            VisibleInMenu = true
                        });
                });

            modelBuilder.Entity("cms.models.styling.Background", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BackgroundImageId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BackgroundImageId");

                    b.ToTable("Background");
                });

            modelBuilder.Entity("cms.models.styling.ContainerStyling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("BackgroundId")
                        .HasColumnType("integer");

                    b.Property<float?>("Height")
                        .HasColumnType("real");

                    b.Property<int?>("MarginId")
                        .HasColumnType("integer");

                    b.Property<int?>("PaddingId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("BackgroundId");

                    b.HasIndex("MarginId");

                    b.HasIndex("PaddingId");

                    b.ToTable("ContainerStyling");
                });

            modelBuilder.Entity("cms.models.styling.ImageStyle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ImageFileId")
                        .HasColumnType("integer");

                    b.Property<string>("ObjectFit")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ImageFileId");

                    b.ToTable("ImageStyle");
                });

            modelBuilder.Entity("cms.models.styling.Margin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Bottom")
                        .HasColumnType("integer");

                    b.Property<int?>("Left")
                        .HasColumnType("integer");

                    b.Property<int?>("Right")
                        .HasColumnType("integer");

                    b.Property<int?>("Top")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Margin");
                });

            modelBuilder.Entity("cms.models.styling.Padding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Bottom")
                        .HasColumnType("integer");

                    b.Property<int?>("Left")
                        .HasColumnType("integer");

                    b.Property<int?>("Right")
                        .HasColumnType("integer");

                    b.Property<int?>("Top")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Padding");
                });

            modelBuilder.Entity("cms.models.ImageFile", b =>
                {
                    b.HasBaseType("cms.models.CmsFile");

                    b.Property<float>("AspectRatio")
                        .HasColumnType("real");

                    b.Property<int[]>("Sizes")
                        .IsRequired()
                        .HasColumnType("integer[]");

                    b.ToTable("ImageFiles", (string)null);
                });

            modelBuilder.Entity("cms.models.ImageComponent", b =>
                {
                    b.HasBaseType("cms.models.Component");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.ToTable("ImageComponents", (string)null);
                });

            modelBuilder.Entity("cms.models.TextComponent", b =>
                {
                    b.HasBaseType("cms.models.Component");

                    b.Property<string>("EditorState")
                        .HasColumnType("text");

                    b.Property<string>("Html")
                        .HasColumnType("text");

                    b.ToTable("TextComponents", (string)null);
                });

            modelBuilder.Entity("cms.ecommerce.models.Product", b =>
                {
                    b.HasOne("cms.ecommerce.models.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryId");

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("cms.ecommerce.models.ProductCategory", b =>
                {
                    b.HasOne("cms.ecommerce.models.ProductCategory", "ParentCategory")
                        .WithMany("Subcategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("cms.ecommerce.models.ProductField", b =>
                {
                    b.HasOne("cms.ecommerce.models.Product", null)
                        .WithMany("ProductFields")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("cms.models.CmsFile", b =>
                {
                    b.HasOne("cms.models.FileFolder", "FileFolder")
                        .WithMany("Files")
                        .HasForeignKey("FileFolderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FileFolder");
                });

            modelBuilder.Entity("cms.models.FileFolder", b =>
                {
                    b.HasOne("cms.models.FileFolder", "ParentFolder")
                        .WithMany("Subfolders")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentFolder");
                });

            modelBuilder.Entity("cms.models.GlobalFooter", b =>
                {
                    b.HasOne("cms.models.GridContent", "GridContent")
                        .WithMany()
                        .HasForeignKey("GridContentId");

                    b.Navigation("GridContent");
                });

            modelBuilder.Entity("cms.models.GlobalHeader", b =>
                {
                    b.HasOne("cms.models.GridContent", "GridContent")
                        .WithMany()
                        .HasForeignKey("GridContentId");

                    b.Navigation("GridContent");
                });

            modelBuilder.Entity("cms.models.GridColumn", b =>
                {
                    b.HasOne("cms.models.Component", "Component")
                        .WithMany()
                        .HasForeignKey("ComponentId");

                    b.HasOne("cms.models.GridContent", "GridContent")
                        .WithMany()
                        .HasForeignKey("GridContentId");

                    b.HasOne("cms.models.GridRow", "GridRow")
                        .WithMany("Columns")
                        .HasForeignKey("GridRowId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("cms.models.styling.ContainerStyling", "Styling")
                        .WithMany()
                        .HasForeignKey("StylingId");

                    b.Navigation("Component");

                    b.Navigation("GridContent");

                    b.Navigation("GridRow");

                    b.Navigation("Styling");
                });

            modelBuilder.Entity("cms.models.GridRow", b =>
                {
                    b.HasOne("cms.models.GridContent", null)
                        .WithMany("GridRows")
                        .HasForeignKey("GridContentId");

                    b.HasOne("cms.models.styling.ContainerStyling", "Styling")
                        .WithMany()
                        .HasForeignKey("StylingId");

                    b.Navigation("Styling");
                });

            modelBuilder.Entity("cms.models.Page", b =>
                {
                    b.HasOne("cms.models.GridContent", "GridContent")
                        .WithMany()
                        .HasForeignKey("GridContentId");

                    b.HasOne("cms.models.Page", "ParentPage")
                        .WithMany("Children")
                        .HasForeignKey("ParentPageId");

                    b.Navigation("GridContent");

                    b.Navigation("ParentPage");
                });

            modelBuilder.Entity("cms.models.styling.Background", b =>
                {
                    b.HasOne("cms.models.styling.ImageStyle", "BackgroundImage")
                        .WithMany()
                        .HasForeignKey("BackgroundImageId");

                    b.Navigation("BackgroundImage");
                });

            modelBuilder.Entity("cms.models.styling.ContainerStyling", b =>
                {
                    b.HasOne("cms.models.styling.Background", "Background")
                        .WithMany()
                        .HasForeignKey("BackgroundId");

                    b.HasOne("cms.models.styling.Margin", "Margin")
                        .WithMany()
                        .HasForeignKey("MarginId");

                    b.HasOne("cms.models.styling.Padding", "Padding")
                        .WithMany()
                        .HasForeignKey("PaddingId");

                    b.Navigation("Background");

                    b.Navigation("Margin");

                    b.Navigation("Padding");
                });

            modelBuilder.Entity("cms.models.styling.ImageStyle", b =>
                {
                    b.HasOne("cms.models.ImageFile", "ImageFile")
                        .WithMany()
                        .HasForeignKey("ImageFileId");

                    b.Navigation("ImageFile");
                });

            modelBuilder.Entity("cms.models.ImageFile", b =>
                {
                    b.HasOne("cms.models.CmsFile", null)
                        .WithOne()
                        .HasForeignKey("cms.models.ImageFile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cms.models.ImageComponent", b =>
                {
                    b.HasOne("cms.models.Component", null)
                        .WithOne()
                        .HasForeignKey("cms.models.ImageComponent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cms.models.TextComponent", b =>
                {
                    b.HasOne("cms.models.Component", null)
                        .WithOne()
                        .HasForeignKey("cms.models.TextComponent", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("cms.ecommerce.models.Product", b =>
                {
                    b.Navigation("ProductFields");
                });

            modelBuilder.Entity("cms.ecommerce.models.ProductCategory", b =>
                {
                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("cms.models.FileFolder", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Subfolders");
                });

            modelBuilder.Entity("cms.models.GridContent", b =>
                {
                    b.Navigation("GridRows");
                });

            modelBuilder.Entity("cms.models.GridRow", b =>
                {
                    b.Navigation("Columns");
                });

            modelBuilder.Entity("cms.models.Page", b =>
                {
                    b.Navigation("Children");
                });
#pragma warning restore 612, 618
        }
    }
}