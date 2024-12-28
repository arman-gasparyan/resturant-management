using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Persistence;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Infrastructure.Repositories
{
    /// <summary>
    /// Implements IReservationRepository using Entity Framework Core.
    /// </summary>
    public class ReservationRepository(RestaurantReservationDbContext context) : IReservationRepository
    {
        /// <inheritdoc />
        public async Task AddAsync(Reservation reservation, CancellationToken cancellationToken = default)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            await context.Reservations.AddAsync(reservation, cancellationToken);
        }
        
        /// <inheritdoc />
        public async Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Reservations
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<List<Reservation>> GetByTableIdAsync(Guid tableId, CancellationToken cancellationToken = default)
        {
            return await context.Reservations
                .Where(r => r.TableId == tableId)
                .Include(r => r.Table)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public void Remove(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            context.Reservations.Remove(reservation);
        }

        /// <inheritdoc />
        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public void Update(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            context.Reservations.Update(reservation);
        }

        /// <inheritdoc />
        public Task<bool> HasOverlappingReservationAsync(
            Guid tableId, 
            DateTime startTime, 
            DateTime endTime, 
            CancellationToken cancellationToken = default)
        {
            if (startTime >= endTime)
                throw new ArgumentException("Start time must be earlier than end time.");

            var utcStartTime = startTime.ToUniversalTime();
            var utcEndTime = endTime.ToUniversalTime();

            return context.Reservations
                .Where(r => r.TableId == tableId &&
                            r.ReservationTime < utcEndTime &&
                            r.ReservationTime > utcStartTime)
                .AnyAsync(cancellationToken);
        }
    }
}