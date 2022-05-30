using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace PTDuc.WhereHouse.DBContext.Models
{
    public partial class WhereHouseContext : DbContext
    {
        public WhereHouseContext()
        {
        }

        public WhereHouseContext(DbContextOptions<WhereHouseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<HouseType> HouseTypes { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=PTDUC1;Initial Catalog=WhereHouse;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<House>(entity =>
            {
                entity.ToTable("House");

                entity.Property(e => e.HouseId).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasComment("Fix tạm cột địa chỉ này, sau sẽ đổi thành các cột riêng chọn Id vị trí ");

                entity.Property(e => e.HouseName).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(22, 4)");

                entity.HasOne(d => d.HouseType)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.HouseTypeId)
                    .HasConstraintName("FK_House_HouseType");

                entity.HasOne(d => d.UserOwner)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.UserOwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_House_User");
            });

            modelBuilder.Entity<HouseType>(entity =>
            {
                entity.ToTable("HouseType");

                entity.Property(e => e.HouseTypeId).ValueGeneratedNever();

                entity.Property(e => e.HouseTypeName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).ValueGeneratedNever();

                entity.Property(e => e.Descrtiption)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.House)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.HouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_House");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("decimal(22, 4)");

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DistrictCode).HasMaxLength(10);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ProvinceCode).HasMaxLength(10);

                entity.Property(e => e.Role).HasComment("Vai trò(1 - admin, 2-user)");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.WardCode).HasMaxLength(10);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
