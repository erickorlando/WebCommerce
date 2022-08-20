using Microsoft.EntityFrameworkCore;
using WebCommerce.Entities.Infos;

namespace WebCommerce.DataAccess
{
    public partial class MusicStoreDbContext : DbContext
    {
        public MusicStoreDbContext()
        {
        }

        public MusicStoreDbContext(DbContextOptions<MusicStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Concert> Concerts { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Sale> Sales { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=MusicStoreDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Concert>(entity =>
            {
                entity.ToTable("Concert");

                entity.HasIndex(e => e.GenreId, "IX_Concert_GenreId");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Place).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Concerts)
                    .HasForeignKey(d => d.GenreId);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.HasIndex(e => e.ConcertId, "IX_Sale_ConcertId");

                entity.HasIndex(e => e.UserId, "IX_Sale_UserId");

                entity.Property(e => e.OperationNumber).HasMaxLength(8);

                entity.Property(e => e.SaleDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasDefaultValueSql("(CONVERT([bit],(1)))");

                entity.Property(e => e.TotalSale).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.HasOne(d => d.Concert)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.ConcertId);
            });

            modelBuilder.Entity<ConcertInfo>()
                .HasNoKey()
                .Property(p => p.UnitPrice)
                .HasPrecision(11, 2);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
