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

        public virtual DbSet<Conversation> Conversations { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<House> Houses { get; set; }
        public virtual DbSet<HouseType> HouseTypes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Wishlist> Wishlists { get; set; }

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

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversation");

                entity.Property(e => e.ConversationId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.ConversationUserId1Navigations)
                    .HasForeignKey(d => d.UserId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conversation_User");

                entity.HasOne(d => d.UserId2Navigation)
                    .WithMany(p => p.ConversationUserId2Navigations)
                    .HasForeignKey(d => d.UserId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conversation_User1");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.ToTable("File");

                entity.Property(e => e.FileId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FilePath).HasMaxLength(255);
            });

            modelBuilder.Entity<House>(entity =>
            {
                entity.ToTable("House");

                entity.Property(e => e.HouseId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.HouseName).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(22, 4)");

                entity.HasOne(d => d.HouseImage)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.HouseImageId)
                    .HasConstraintName("FK_House_File");

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

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId).ValueGeneratedOnAdd();

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationId)
                    .HasConstraintName("FK_Message_Conversation");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_User");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Descrtiption).HasMaxLength(255);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

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

                entity.Property(e => e.UserId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DistrictCode).HasMaxLength(10);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);

                entity.Property(e => e.ProvinceCode).HasMaxLength(10);

                entity.Property(e => e.Role)
                    .HasDefaultValueSql("((1))")
                    .HasComment("Vai trò(1 - admin, 2-user)");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.WardCode).HasMaxLength(10);

                entity.HasOne(d => d.Avatar)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AvatarId)
                    .HasConstraintName("FK_User_File");
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.ToTable("Wishlist");

                entity.Property(e => e.WishlistId).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedBy).HasMaxLength(255);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wishlish_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wishlists)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wishlish_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
