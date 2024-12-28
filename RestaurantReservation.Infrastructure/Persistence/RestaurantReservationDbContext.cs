using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Infrastructure.Persistence
{
    public class RestaurantReservationDbContext(DbContextOptions<RestaurantReservationDbContext> options)
        : DbContext(options)
    {
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.ToTable("Restaurants");

                entity.HasKey(r => r.Id);

                entity.Property(r => r.TenantId)
                    .IsRequired();

                entity.Property(r => r.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(r => r.OpeningTime)
                    .IsRequired();

                entity.Property(r => r.ClosingTime)
                    .IsRequired();

                entity.Property(r => r.DefaultReservationDuration)
                    .IsRequired();

                entity.Property(r => r.BufferTime)
                    .IsRequired();
                
                entity.Property(r => r.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();
                
                entity.HasMany(r => r.Tables)
                    .WithOne(t => t.Restaurant)
                    .HasForeignKey(t => t.RestaurantId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(r => new { r.OpeningTime, r.ClosingTime })
                    .HasDatabaseName("IX_Restaurant_OperatingHours");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.ToTable("Tables");
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Location)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(t => t.NumberOfSeats)
                    .IsRequired();

                entity.HasMany(t => t.Reservations)
                    .WithOne(r => r.Table)
                    .HasForeignKey(r => r.TableId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(t => t.Location)
                    .HasDatabaseName("IX_Table_Name");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservations");
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(r => r.ReservationTime)
                    .IsRequired();

                entity.Property(r => r.Duration)
                    .IsRequired();

                entity.Property(r => r.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.HasIndex(r => new { r.TableId, r.ReservationTime })
                    .HasDatabaseName("IX_Reservation_TableId_ReservationTime");
            });
        }
    }
}