using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Repositories
{
    /// <summary>
    /// Defines methods for accessing and manipulating Table entities.
    /// </summary>
    public interface ITableRepository
    {
        /// <summary>
        /// Retrieves a table by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the table.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The Table entity if found; otherwise, null.</returns>
        Task<Table?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves all tables.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of all Table entities.</returns>
        Task<List<Table>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Retrieves tables by a list of identifiers.
        /// </summary>
        /// <param name="ids">List of table unique identifiers.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A list of matching Table entities.</returns>
        Task<List<Table>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default);

        /// <summary>
        /// Adds a new table to the repository.
        /// </summary>
        /// <param name="table">The Table entity to add.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(Table table, CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates an existing table in the repository.
        /// </summary>
        /// <param name="table">The Table entity to update.</param>
        void Update(Table table);

        /// <summary>
        /// Removes a table from the repository.
        /// </summary>
        /// <param name="table">The Table entity to remove.</param>
        void Remove(Table table);

        /// <summary>
        /// Saves all changes made in the context to the database.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}