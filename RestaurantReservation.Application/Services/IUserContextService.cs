// Location: RestaurantReservation.Application.Services/IUserContextService.cs

namespace RestaurantReservation.Application.Services
{
    /// <summary>
    /// Provides information about the currently authenticated user.
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Gets the unique identifier of the current user.
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// Gets the tenant identifier associated with the current user.
        /// </summary>
        string? TenantId { get; }
    }
}