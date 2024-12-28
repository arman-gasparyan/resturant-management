using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using RestaurantReservation.Application.Services;
using RestaurantReservation.Domain.Repositories;
using RestaurantReservation.Infrastructure.Authorization;

namespace RestaurantReservation.Web.Api.Authorization
{
    /// <summary>
    /// Authorization handler to ensure users belong to the tenant associated with the resource.
    /// </summary>
    public class TenantAuthorizationHandler(
        IUserContextService userContextService,
        IRestaurantRepository restaurantRepository)
        : AuthorizationHandler<TenantAuthorizationRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TenantAuthorizationRequirement requirement)
        {
            if (context.Resource is not HttpContext httpContext)
            {
                context.Fail();
                return;
            }

            if (!httpContext.Request.RouteValues.TryGetValue("restaurantId", out var idValue) ||
                !Guid.TryParse(idValue?.ToString(), out var restaurantId))
            {
                context.Fail();
                return;
            }

            var restaurant = await restaurantRepository.GetByIdAsync(restaurantId);

            if (restaurant == null)
            {
                context.Fail();
                return;
            }
            if (restaurant.TenantId == userContextService.TenantId)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }

    }
}
