using EventCatalogApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogApi.Data
{
    public class CatalogContext :DbContext
    {
        public CatalogContext(DbContextOptions options):base (options)
        {

        }

        public DbSet<CatalogCategory> CatalogCategories { get; set; }

        public DbSet<CatalogType> CatalogTypes { get; set; }

        public DbSet<CatalogCity> CatalogCities { get; set; }

        public DbSet<CatalogEvent> CatalogEvents { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogCategory>(ConfigureCatalogCategory);
            modelBuilder.Entity<CatalogType>(ConfigureCatalogType);
            modelBuilder.Entity<CatalogCity>(ConfigureCatalogCity);
            modelBuilder.Entity<CatalogEvent>(ConfigureCatalogEvent);
        }

        private void ConfigureCatalogCity(EntityTypeBuilder<CatalogCity> builder)
        {
            builder.ToTable("CatalogCities");
            builder.Property(c => c.ID)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("catalog_city_hilo");
            builder.Property(c => c.City)
                .IsRequired();                

        }

        private void ConfigureCatalogCategory(EntityTypeBuilder<CatalogCategory> builder)
        {
            builder.ToTable("CatalogCategories");
            builder.Property(c => c.ID)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("catalog_category_hilo");
            builder.Property(c => c.Category)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.PictureUrl)
                .IsRequired();

        }

        private void ConfigureCatalogType(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("CatalogTypes");
            builder.Property(c => c.ID)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("catalog_type_hilo");
            builder.Property(c => c.Type)
                .IsRequired();
           
        }

        private void ConfigureCatalogEvent(EntityTypeBuilder<CatalogEvent> builder)
        {
            builder.ToTable("Catalog");
            builder.Property(c => c.ID)
                .IsRequired()
                .ForSqlServerUseSequenceHiLo("catalog_event_hilo");
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.State)
                .IsRequired();
            builder.Property(c => c.City)
                .IsRequired();
            builder.Property(c => c.Zipcode)
               .IsRequired();
            builder.Property(c => c.StartDate)
                .IsRequired();
            builder.Property(c => c.EndDate)
               .IsRequired();
            
            
            //builder.Property(c => c.Month)
            //    .IsRequired();
            //builder.Property(c => c.Date)
            //    .IsRequired();
            //builder.Property(c => c.Day)
            //.IsRequired();
          
            //builder.Property(c => c.Time)
            //.IsRequired();
            builder.Property(c => c.Price)
                .IsRequired();
            builder.HasOne(c => c.CatalogCategory)
                .WithMany()
                .HasForeignKey(c => c.CatalogCategoryID);

            builder.HasOne(c => c.CatalogType)
               .WithMany()
               .HasForeignKey(c => c.CatalogTypeID);

            builder.HasOne(c => c.CatalogCity)
                           .WithMany()
                           .HasForeignKey(c => c.CatalogCityID);

        }

    }
}
