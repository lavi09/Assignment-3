﻿// <auto-generated />
using EventCatalogApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EventCatalogApi.Migrations
{
    [DbContext(typeof(CatalogContext))]
    partial class CatalogContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("Relational:Sequence:.catalog_category_hilo", "'catalog_category_hilo', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.catalog_city_hilo", "'catalog_city_hilo', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.catalog_event_hilo", "'catalog_event_hilo', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Relational:Sequence:.catalog_type_hilo", "'catalog_type_hilo', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EventCatalogApi.Domain.CatalogCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "catalog_category_hilo")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.ToTable("CatalogCategories");
                });

            modelBuilder.Entity("EventCatalogApi.Domain.CatalogCity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "catalog_city_hilo")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("City")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("CatalogCities");
                });

            modelBuilder.Entity("EventCatalogApi.Domain.CatalogEvent", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "catalog_event_hilo")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("CatalogCategoryID");

                    b.Property<int>("CatalogCityID");

                    b.Property<int>("CatalogTypeID");

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<string>("Date")
                        .IsRequired();

                    b.Property<string>("Day")
                        .IsRequired();

                    b.Property<string>("Month")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("PictureUrl");

                    b.Property<decimal>("Price");

                    b.Property<string>("State")
                        .IsRequired();

                    b.Property<string>("Time")
                        .IsRequired();

                    b.HasKey("ID");

                    b.HasIndex("CatalogCategoryID");

                    b.HasIndex("CatalogCityID");

                    b.HasIndex("CatalogTypeID");

                    b.ToTable("Catalog");
                });

            modelBuilder.Entity("EventCatalogApi.Domain.CatalogType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "catalog_type_hilo")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("CatalogTypes");
                });

            modelBuilder.Entity("EventCatalogApi.Domain.CatalogEvent", b =>
                {
                    b.HasOne("EventCatalogApi.Domain.CatalogCategory", "CatalogCategory")
                        .WithMany()
                        .HasForeignKey("CatalogCategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EventCatalogApi.Domain.CatalogCity", "CatalogCity")
                        .WithMany()
                        .HasForeignKey("CatalogCityID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EventCatalogApi.Domain.CatalogType", "CatalogType")
                        .WithMany()
                        .HasForeignKey("CatalogTypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
