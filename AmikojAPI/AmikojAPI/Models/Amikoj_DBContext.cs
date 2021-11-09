using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AmikojApi.Models
{
    public partial class Amikoj_DBContext : DbContext
    {
        public Amikoj_DBContext()
        {
        }

        public Amikoj_DBContext(DbContextOptions<Amikoj_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual DbSet<ChapterModel> Chapters { get; set; }
        public virtual DbSet<ClassesModel> Classes { get; set; }
        public virtual DbSet<ClassResultModel> ClassResult { get; set; }
        public virtual DbSet<ConversationModel> Conversations { get; set; }
        public virtual DbSet<CourseModel> Courses { get; set; }
        public virtual DbSet<SentenceModel> Sentences { get; set; }
        public virtual DbSet<UsersProgress> UsersProgresses { get; set; }
        public virtual DbSet<WordModel> Words { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=tcp:amikojapidbserver.database.windows.net,1433;Initial Catalog=AmikojAPI_db;User Id=nimesttech@amikojapidbserver;Password=micrNimest2017**");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");


            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Discriminator).IsRequired();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(150);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<ChapterModel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.MyLangCode)
                    .HasMaxLength(2)
                    .HasColumnName("MyLangCode");

                entity.Property(e => e.LearnLangCode)
                    .HasMaxLength(2)
                    .HasColumnName("LearnLangCode");

                entity.Property(e => e.ChapterName)
                    .HasMaxLength(100)
                    .HasColumnName("ChapterName");

                entity.Property(e => e.ChapterDescription)
                    .HasMaxLength(500)
                    .HasColumnName("ChapterDescription");

                entity.Property(e => e.TranslateName)
                    .HasMaxLength(100)
                    .HasColumnName("TranslateName");

                entity.Property(e => e.TranslateDescription)
                    .HasMaxLength(500)
                    .HasColumnName("TranslateDescription");
            });

            modelBuilder.Entity<ClassesModel>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__Classes__58A2D5BD5F91795D");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.LearnLangCode)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MyLangCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.Level).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<ConversationModel>(entity =>
            {
                entity.ToTable("Conversation");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CharacterName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Conversation)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("Conversation");

                entity.Property(e => e.MyLangCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Tips).HasMaxLength(500);

                entity.Property(e => e.Translation)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.LearnLangCode)
                    .IsRequired()
                    .HasMaxLength(2);
            });

            modelBuilder.Entity<CourseModel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CourseCode)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsFixedLength(true);

                entity.Property(e => e.CourseFullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CourseShortName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LearnLangCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.MyLangCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<SentenceModel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Sentence)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("Sentence");

                entity.Property(e => e.MyLangCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Tips).HasMaxLength(500);

                entity.Property(e => e.LearnLangCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Translation)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<UsersProgress>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("PK__UsersPro__BAE29CA5EDFCD824");

                entity.ToTable("UsersProgress");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.MyLangCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);

                entity.Property(e => e.LearnLangCode)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<WordModel>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Tips).HasMaxLength(500);

                entity.Property(e => e.LearnLangCode)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.Translation)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Word");

                entity.Property(e => e.MyLangCode)
                    .IsRequired()
                    .HasMaxLength(2);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
