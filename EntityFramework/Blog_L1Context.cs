//using System;
//using BlogCore.Core.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace BlogCore.EntityFramework
//{
//    public partial class Blog_L1Context : DbContext
//    {
//        public virtual DbSet<Category> Category { get; set; }
//        public virtual DbSet<File> File { get; set; }
//        public virtual DbSet<MovieTest> MovieTest { get; set; }
//        public virtual DbSet<Post> Post { get; set; }
//        public virtual DbSet<PostCategory> PostCategory { get; set; }
//        public virtual DbSet<PostFile> PostFile { get; set; }
//        public virtual DbSet<PostTag> PostTag { get; set; }
//        public virtual DbSet<Tag> Tag { get; set; }
//        public virtual DbSet<User> User { get; set; }
//        public virtual DbSet<Ware> Ware { get; set; }
//        public virtual DbSet<WareFile> WareFile { get; set; }

//        public Blog_L1Context(DbContextOptions<Blog_L1Context> options)
//     : base(options) { }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Category>(entity =>
//            {
//                entity.Property(e => e.CategoryName)
//                    .IsRequired()
//                    .HasMaxLength(50);

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.ModifyDate).HasColumnType("datetime");
//            });

//            modelBuilder.Entity<File>(entity =>
//            {
//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.Path)
//                    .IsRequired()
//                    .HasMaxLength(50);

//                entity.Property(e => e.Type)
//                    .IsRequired()
//                    .HasMaxLength(50);
//            });

//            modelBuilder.Entity<MovieTest>(entity =>
//            {
//                entity.ToTable("Movie.Test");

//                entity.Property(e => e.Id).ValueGeneratedNever();

//                entity.Property(e => e.Movie).HasMaxLength(50);
//            });

//            modelBuilder.Entity<Post>(entity =>
//            {
//                entity.Property(e => e.Content).IsRequired();

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

//                entity.Property(e => e.PostDate).HasColumnType("datetime");

//                entity.Property(e => e.ShortContent).IsRequired();

//                entity.Property(e => e.Title)
//                    .IsRequired()
//                    .HasMaxLength(50);
//            });

//            modelBuilder.Entity<PostCategory>(entity =>
//            {
//                entity.HasOne(d => d.Category)
//                    .WithMany(p => p.PostCategory)
//                    .HasForeignKey(d => d.CategoryId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__PostCateg__Categ__628FA481");

//                entity.HasOne(d => d.Post)
//                    .WithMany(p => p.PostCategory)
//                    .HasForeignKey(d => d.PostId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__PostCateg__PostI__6383C8BA");
//            });

//            modelBuilder.Entity<PostFile>(entity =>
//            {
//                entity.HasOne(d => d.File)
//                    .WithMany(p => p.PostFile)
//                    .HasForeignKey(d => d.FileId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__PostFile__FileId__6477ECF3");

//                entity.HasOne(d => d.Post)
//                    .WithMany(p => p.PostFile)
//                    .HasForeignKey(d => d.PostId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__PostFile__PostId__656C112C");
//            });

//            modelBuilder.Entity<PostTag>(entity =>
//            {
//                entity.HasOne(d => d.Post)
//                    .WithMany(p => p.PostTag)
//                    .HasForeignKey(d => d.PostId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__PostTag__PostId__66603565");

//                entity.HasOne(d => d.Tag)
//                    .WithMany(p => p.PostTag)
//                    .HasForeignKey(d => d.TagId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__PostTag__TagId__6754599E");
//            });

//            modelBuilder.Entity<Tag>(entity =>
//            {
//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

//                entity.Property(e => e.TagName)
//                    .IsRequired()
//                    .HasMaxLength(50);
//            });

//            modelBuilder.Entity<User>(entity =>
//            {
//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.Email)
//                    .IsRequired()
//                    .HasMaxLength(50);

//                entity.Property(e => e.LockoutEndDateUtc).HasColumnType("datetime");

//                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

//                entity.Property(e => e.PasswordHash)
//                    .IsRequired()
//                    .HasMaxLength(50);

//                entity.Property(e => e.UserName)
//                    .IsRequired()
//                    .HasMaxLength(50);
//            });

//            modelBuilder.Entity<Ware>(entity =>
//            {
//                entity.Property(e => e.BuyDate).HasColumnType("datetime");

//                entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

//                entity.Property(e => e.Title)
//                    .IsRequired()
//                    .HasMaxLength(50);
//            });

//            modelBuilder.Entity<WareFile>(entity =>
//            {
//                entity.HasOne(d => d.File)
//                    .WithMany(p => p.WareFile)
//                    .HasForeignKey(d => d.FileId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__WareFile__FileId__68487DD7");

//                entity.HasOne(d => d.Ware)
//                    .WithMany(p => p.WareFile)
//                    .HasForeignKey(d => d.WareId)
//                    .OnDelete(DeleteBehavior.ClientSetNull)
//                    .HasConstraintName("FK__WareFile__WareId__693CA210");
//            });
//        }
//    }
//}
