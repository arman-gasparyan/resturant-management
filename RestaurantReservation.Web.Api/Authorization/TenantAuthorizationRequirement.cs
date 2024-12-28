using Microsoft.AspNetCore.Authorization;

namespace RestaurantReservation.Infrastructure.Authorization
{
    /// <summary>
    /// Requirement that the user must belong to the tenant associated with the resource.
    /// </summary>
    public class TenantAuthorizationRequirement : IAuthorizationRequirement
    {
    }
}