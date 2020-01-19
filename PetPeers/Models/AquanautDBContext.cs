using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PetPeers.Models
{
    public partial class AquanautDBContext : DbContext
    {
        public AquanautDBContext()
        {
        }

        public AquanautDBContext(DbContextOptions<AquanautDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pets> Pets { get; set; }
        public virtual DbSet<PetsTransaction> PetsTransaction { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=40.71.199.35;Database=AquanautDB;User Id=kantarsql;Password=hackathon@201001");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Pets>(entity =>
            {
                entity.ToTable("PETS");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.OwnerId).HasMaxLength(10);

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<PetsTransaction>(entity =>
            {
                entity.ToTable("PETS_TRANSACTION");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.PetId).HasColumnName("PetID");

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.PetsTransaction)
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRANSACTION_PETS");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PetsTransaction)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TRANSACTION_USERS");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("USERS");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasMaxLength(250);
            });
        }
    }
}
