using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConnectTheDotsService.Models
{
    public partial class ConnectTheDotsContext : DbContext
    {
        public ConnectTheDotsContext()
        {
        }

        public ConnectTheDotsContext(DbContextOptions<ConnectTheDotsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Connection> Connection { get; set; }
        public virtual DbSet<Dot> Dot { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=LocalHost;Database=ConnectTheDots;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Connection>(entity =>
            {
                entity.Property(e => e.ConnectionDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.DotSourceNavigation)
                    .WithMany(p => p.Connection)
                    .HasForeignKey(d => d.DotSource)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo_Connection_DotSyncId_dbo_Dot");
            });

            modelBuilder.Entity<Dot>(entity =>
            {
                entity.Property(e => e.DotName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Xcoordinate)
                    .HasColumnName("XCoordinate")
                    .HasColumnType("decimal(18, 6)");

                entity.Property(e => e.Ycoordinate)
                    .HasColumnName("YCoordinate")
                    .HasColumnType("decimal(18, 6)");
            });
        }
    }
}
