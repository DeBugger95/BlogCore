using System;
using BlogCore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BlogCore.EntityFramework
{
    public partial class Blog_L2Context : DbContext
    {
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<File> File { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<PostCategory> PostCategory { get; set; }
        public virtual DbSet<PostFile> PostFile { get; set; }
        public virtual DbSet<PostTag> PostTag { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Ware> Ware { get; set; }
        public virtual DbSet<WareFile> WareFile { get; set; }

        public Blog_L2Context(DbContextOptions<Blog_L2Context> options)
        : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<File>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.Property(e => e.ShortContent).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PostCategory>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.CategoryId });

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.PostCategory)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostCateg__Categ__6E01572D");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostCategory)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostCateg__PostI__6EF57B66");
            });

            modelBuilder.Entity<PostFile>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.FileId });

                entity.HasOne(d => d.File)
                    .WithMany(p => p.PostFile)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostFile__FileId__6FE99F9F");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostFile)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostFile__PostId__70DDC3D8");
            });

            modelBuilder.Entity<PostTag>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.PostId });

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostTag)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostTag__PostId__71D1E811");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.PostTag)
                    .HasForeignKey(d => d.TagId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostTag__TagId__72C60C4A");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.TagName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ware>(entity =>
            {
                entity.Property(e => e.BuyDate).HasColumnType("datetime");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WareFile>(entity =>
            {
                entity.HasKey(e => new { e.WareId, e.FileId });

                entity.HasOne(d => d.File)
                    .WithMany(p => p.WareFile)
                    .HasForeignKey(d => d.FileId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WareFile__FileId__73BA3083");

                entity.HasOne(d => d.Ware)
                    .WithMany(p => p.WareFile)
                    .HasForeignKey(d => d.WareId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__WareFile__WareId__74AE54BC");
            });
        }
    }
}
