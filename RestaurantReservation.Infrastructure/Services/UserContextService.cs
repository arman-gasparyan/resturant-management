using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using RestaurantReservation.Application.Services;

namespace RestaurantReservation.Infrastructure.Services
{
    /// <summary>
    /// Implementation of IUserContextService that retrieves user information from the current HTTP context.
    /// </summary>
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContextService"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Accessor for the current HTTP context.</param>
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public string? UserId =>
            _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

        /// <inheritdoc />
        public string? TenantId =>
            _httpContextAccessor
                .HttpContext?
                .User?
                .FindFirst("aud")?
                .Value;
    }
}