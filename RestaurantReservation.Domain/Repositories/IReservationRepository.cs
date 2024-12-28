using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Repositories
{
    /// <summary>
    /// Defines methods for accessing and manipulating Reservation entities.
    /// </summary>
    public interface IReservationRepository
    {
        /// <summary>
        /// Retrieves a reservation by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the reservation.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The Reservation entity if found; otherwise, null.</returns>
        Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Retrieves reservations by a specific table identifier.
        /// </summary>
        /// <param name="tableId">The unique identifier of the table.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of Reservation entities associated with the specified table.</returns>
        Task<List<Reservation>> GetByTableIdAsync(Guid tableId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new reservation to the repository.
        /// </summary>
        /// <param name="reservation">The Reservation entity to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Reservation reservation, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing reservation in the repository.
        /// </summary>
        /// <param name="reservation">The Reservation entity to update.</param>
        void Update(Reservation reservation);

        /// <summary>
        /// Removes a reservation from the repository.
        /// </summary>
        /// <param name="reservation">The Reservation entity to remove.</param>
        void Remove(Reservation reservation);

        /// <summary>
        /// Saves all changes made in the context to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Checks if there is any overlapping reservation for the given table and time frame.
        /// </summary>
        /// <param name="tableId">The ID of the table.</param>
        /// <param name="startTime">The desired start time for the reservation.</param>
        /// <param name="endTime">The desired end time for the reservation.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>True if an overlapping reservation exists; otherwise, false.</returns>
        Task<bool> HasOverlappingReservationAsync(Guid tableId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken = default);
    }
}