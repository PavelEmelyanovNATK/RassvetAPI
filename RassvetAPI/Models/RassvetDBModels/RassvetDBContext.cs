using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RassvetAPI.Models.RassvetDBModels
{
    public partial class RassvetDBContext : DbContext
    {
        public RassvetDBContext()
        {
        }

        public RassvetDBContext(DbContextOptions<RassvetDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ClientInfo> ClientInfos { get; set; }
        public virtual DbSet<ClientToTraining> ClientToTrainings { get; set; }
        public virtual DbSet<ClientToSection> ClientToSections { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<TrenerInfo> TrenerInfos { get; set; }
        public virtual DbSet<TrenerToTraining> TrenerToTrainings { get; set; }
        public virtual DbSet<Training> Trainings { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<ClientInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_Client");

                entity.ToTable("ClientInfo");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Patronymic).HasMaxLength(50);

                entity.Property(e => e.RegistrationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.ClientInfo)
                    .HasForeignKey<ClientInfo>(d => d.UserId)
                    .HasConstraintName("FK_Client_User");
            });

            modelBuilder.Entity<ClientToSection>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.SectionId });

                entity.ToTable("ClientToSection");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientToSections)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientToSection_ClientInfo");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.ClientToSections)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientToSection_Section");
            });

            modelBuilder.Entity<ClientToTraining>(entity =>
            {
                entity.HasKey(e => new { e.ClientId, e.TrainingId });

                entity.ToTable("ClientToTraining");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.TrainingId).HasColumnName("TrainingID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientToTrainings)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientToTraining_ClientInfo");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.ClientToTrainings)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientToTraining_Training");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RefreshToken_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("Section");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<TrenerInfo>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_Trener");

                entity.ToTable("TrenerInfo");

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("UserID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithOne(p => p.TrenerInfo)
                    .HasForeignKey<TrenerInfo>(d => d.UserId)
                    .HasConstraintName("FK_Trener_User");
            });

            modelBuilder.Entity<TrenerToTraining>(entity =>
            {
                entity.HasKey(e => new { e.TrenerId, e.TrainingId });

                entity.ToTable("TrenerToTraining");

                entity.Property(e => e.TrenerId).HasColumnName("TrenerID");

                entity.Property(e => e.TrainingId).HasColumnName("TrainingID");

                entity.HasOne(d => d.Training)
                    .WithMany(p => p.TrenerToTrainings)
                    .HasForeignKey(d => d.TrainingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrenerToTraining_Training");

                entity.HasOne(d => d.Trener)
                    .WithMany(p => p.TrenerToTrainings)
                    .HasForeignKey(d => d.TrenerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrenerToTraining_TrenerInfo");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email, "UQ_User_Email")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_User_Role");
            });

            modelBuilder.Entity<Training>(entity =>
            {
                entity.ToTable("Training");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Room)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SectionId).HasColumnName("SectionID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Trainings)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Training_Section");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
