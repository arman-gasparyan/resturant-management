using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Persistence;

namespace RestaurantReservation.Infrastructure.Repositories
{
    /// <summary>
    /// Implements ITableRepository using Entity Framework Core.
    /// </summary>
    public class TableRepository(RestaurantReservationDbContext context) : ITableRepository
    {
        public async Task AddAsync(Table table, CancellationToken cancellationToken = default)
        {
            await context.Tables.AddAsync(table, cancellationToken);
        }

        public async Task<List<Table>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Tables
                .Include(t => t.Reservations) 
                .Include(t => t.Restaurant)  
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Table>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
        {
            return await context.Tables
                .Where(t => ids.Contains(t.Id))
                .Include(t => t.Reservations)
                .Include(t => t.Restaurant)
                .ToListAsync(cancellationToken);
        }

        public async Task<Table?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Tables
                .Include(t => t.Reservations)
                .Include(t => t.Restaurant)
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public void Remove(Table table)
        {
            context.Tables.Remove(table);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Table table)
        {
            context.Tables.Update(table);
        }
    }
}