using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SocialMedia.Models
{
    public partial class SociaMediaContext : DbContext
    {
        public SociaMediaContext()
        {
        }

        public SociaMediaContext(DbContextOptions<SociaMediaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CommentPost> CommentPosts { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<InfoUser> InfoUsers { get; set; } = null!;
        public virtual DbSet<LikePost> LikePosts { get; set; } = null!;
        public virtual DbSet<MemberGroup> MemberGroups { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostContent> PostContents { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-2UA9PP0\\SQLEXPRESS;Initial Catalog=SociaMedia;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommentPost>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ContentCommentPost).HasMaxLength(300);

                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPost)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommentPosts_Post");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommentPosts_InfoUser");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey(e => e.IdGroup);

                entity.ToTable("Group");

                entity.Property(e => e.GroupAvatar)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName).HasMaxLength(200);
            });

            modelBuilder.Entity<InfoUser>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("InfoUser");

                entity.Property(e => e.EmailUser)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordUser)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberUser)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserDescription).HasMaxLength(500);

                entity.Property(e => e.UserName).HasMaxLength(200);
            });

            modelBuilder.Entity<LikePost>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPost)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LikePosts_Post");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LikePosts_InfoUser");
            });

            modelBuilder.Entity<MemberGroup>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberGroups_Group");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MemberGroups_InfoUser");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.IdPost);

                entity.ToTable("Post");

                entity.Property(e => e.PostContent).HasMaxLength(500);

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("FK_Post_Group");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_InfoUser");
            });

            modelBuilder.Entity<PostContent>(entity =>
            {
                entity.HasKey(e => e.IdPostContent);

                entity.ToTable("PostContent");

                entity.Property(e => e.UrlimageVideo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("URLImage_video");

                entity.HasOne(d => d.IdPostNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdPost)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostContent_Post");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
