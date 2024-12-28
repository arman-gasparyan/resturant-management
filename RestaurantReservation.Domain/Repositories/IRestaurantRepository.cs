using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Repositories
{
    /// <summary>
    /// Defines methods for accessing and manipulating Restaurant entities.
    /// </summary>
    public interface IRestaurantRepository
    {
        /// <summary>
        /// Retrieves a restaurant by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the restaurant.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The Restaurant entity if found; otherwise, null.</returns>
        Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all restaurants.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of all Restaurant entities.</returns>
        Task<List<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new restaurant to the repository.
        /// </summary>
        /// <param name="restaurant">The Restaurant entity to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing restaurant in the repository.
        /// </summary>
        /// <param name="restaurant">The Restaurant entity to update.</param>
        void Update(Restaurant restaurant);

        /// <summary>
        /// Removes a restaurant from the repository.
        /// </summary>
        /// <param name="restaurant">The Restaurant entity to remove.</param>
        void Remove(Restaurant restaurant);

        /// <summary>
        /// Saves all changes made in the context to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
