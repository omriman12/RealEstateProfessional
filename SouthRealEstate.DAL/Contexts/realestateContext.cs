using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SouthRealEstate.DAL.Entities
{
    public partial class realestateContext : DbContext
    {
        public realestateContext()
        {
        }

        public realestateContext(DbContextOptions<realestateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<PropertiesResidental> PropertiesResidental { get; set; }
        public virtual DbSet<PropertyImages> PropertyImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;user id=user1; password=1qaz2wsx; database=realestate; CharSet=utf8;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cities>(entity =>
            {
                entity.ToTable("cities", "realestate");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PropertiesResidental>(entity =>
            {
                entity.ToTable("properties_residental", "realestate");

                entity.HasIndex(e => e.CityId)
                    .HasName("city_FK_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("bigint(20)");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(100)
                    .IsUnicode(false);

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
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.IsNew)
                    .HasColumnName("is_new")
                    .HasColumnType("tinyint(4)")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SizeMeters)
                    .HasColumnName("size_meters")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.PropertiesResidental)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("city_FK");
            });

            modelBuilder.Entity<PropertyImages>(entity =>
            {
                entity.ToTable("property_images", "realestate");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedNever();

                entity.Property(e => e.ImageName)
                    .HasColumnName("image_name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PropertyId)
                    .HasColumnName("property_id")
                    .HasColumnType("bigint(20)");
            });
        }
    }
}
