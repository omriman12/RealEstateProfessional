using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SouthRealEstate.DAL.Entities
{
    public partial class RealestateContext : DbContext
    {
        public RealestateContext()
        {
        }

        public RealestateContext(DbContextOptions<RealestateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<UmUsers> UmUsers { get; set; }
        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<PropertiesResidental> PropertiesResidental { get; set; }
        public virtual DbSet<PropertiesResidentialImages> PropertiesResidentialImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cities>(entity =>
            {
                entity.ToTable("cities");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");
            });

            modelBuilder.Entity<PropertiesResidental>(entity =>
            {
                entity.ToTable("properties_residental");

                entity.HasIndex(e => e.CityId)
                    .HasName("city_FK_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.BadRoomsCount)
                    .HasColumnName("bad_rooms_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.BathRoomsCount)
                    .HasColumnName("bath_rooms_count")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CityId)
                    .HasColumnName("city_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.IsFeatured)
                    .HasColumnName("is_featured")
                    .HasColumnType("bit(1)");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SizeMeters)
                    .HasColumnName("size_meters")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.City)
                    .WithMany(p => p.PropertiesResidental)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("city_FK");
            });

            modelBuilder.Entity<PropertiesResidentialImages>(entity =>
            {
                entity.ToTable("properties_residential_images");

                entity.HasIndex(e => e.PropertyId)
                    .HasName("property_id_FK_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ImageName)
                    .IsRequired()
                    .HasColumnName("image_name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.PropertyId)
                    .HasColumnName("property_id")
                    .HasColumnType("bigint(20)");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.PropertiesResidentialImages)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("property_id_FK");
            });

            modelBuilder.Entity<UmUsers>(entity =>
            {
                entity.ToTable("um_users");

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("'0'");
            });
        }
    }
}
