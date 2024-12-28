using Microsoft.EntityFrameworkCore;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Persistence;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Infrastructure.Repositories
{
    /// <summary>
    /// Implements IRestaurantRepository using Entity Framework Core.
    /// </summary>
    public class RestaurantRepository(RestaurantReservationDbContext context) : IRestaurantRepository
    {
        public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default)
        {
            await context.Restaurants.AddAsync(restaurant, cancellationToken);
        }

        public async Task<List<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.Restaurants
                .Include(r => r.Tables)
                .ToListAsync(cancellationToken);
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Restaurants
                .Include(r => r.Tables)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public void Remove(Restaurant restaurant)
        {
            context.Restaurants.Remove(restaurant);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        public void Update(Restaurant restaurant)
        {
            context.Restaurants.Update(restaurant);
        }
    }
}