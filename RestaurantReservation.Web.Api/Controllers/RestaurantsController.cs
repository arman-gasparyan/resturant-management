using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using RestaurantReservation.Application.CQRS.Commands;
using RestaurantReservation.Application.CQRS.Queries;

namespace RestaurantReservation.Web.Api.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestaurantsController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR mediator instance.</param>
        public RestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new restaurant.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="openingTime"></param>
        /// <param name="closingTime"></param>
        /// <param name="defaultReservationDuration"></param>
        /// <param name="bufferTime"></param>
        /// <returns>New Restaurant Id</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRestaurant(
            string name,
            TimeSpan openingTime,
            TimeSpan closingTime,
            TimeSpan defaultReservationDuration,
            TimeSpan bufferTime)
        {
            var restaurantId = await _mediator
                .Send(
                    new CreateRestaurantCommand(name, openingTime, closingTime, defaultReservationDuration, bufferTime),
                    HttpContext.RequestAborted);

            return CreatedAtAction(nameof(CreateRestaurant), new { id = restaurantId },
                new { RestaurantId = restaurantId });
        }

        /// <summary>
        /// Deletes a restaurant by Id.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns>HTTP 200 OK on success.</returns>
        [HttpDelete("{restaurantId:guid}")]
        [Authorize(Policy = "TenantPolicy")]
        public async Task<IActionResult> DeleteRestaurant(Guid restaurantId)
        {
            var command = new DeleteRestaurantCommand(restaurantId);
            await _mediator.Send(command, HttpContext.RequestAborted);
            return Ok();
        }

        /// <summary>
        /// Retrieves a restaurant by Id.
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns>RestaurantInfo model</returns>
        [HttpGet("{restaurantId:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetRestaurant(Guid restaurantId)
        {
            var query = new GetRestaurantByIdQuery(restaurantId);
            var restaurant = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(restaurant);
        }

        /// <summary>
        /// Retrieves all restaurants in the system.
        /// </summary>
        /// <returns>List of RestaurantInfo</returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var query = new GetAllRestaurantsQuery();
            var restaurants = await _mediator.Send(query, HttpContext.RequestAborted);
            return Ok(restaurants);
        }

        /// <summary>
        /// Adds a new table to a restaurant.
        /// </summary>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <param name="location"></param>
        /// <param name="numberOfSeats"></param>
        /// <returns>New Table Id</returns>
        [HttpPost("{restaurantId:guid}/tables")]
        [Authorize(Policy = "TenantPolicy")]
        public async Task<IActionResult> AddTable(
            Guid restaurantId, 
            string location,
            int numberOfSeats)
        {
            var tableId = await _mediator.Send(
                new AddTableToRestaurantCommand(restaurantId, location, numberOfSeats), HttpContext.RequestAborted);

            return CreatedAtAction(nameof(AddTable), new { restaurantId, tableId }, new { TableId = tableId });
        }

        /// <summary>
        /// Updates a table for a restaurant.
        /// </summary>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <param name="tableId">Table Id</param>
        /// <param name="command">The command containing updated table details.</param>
        /// <returns>HTTP 200 OK on success.</returns>
        [HttpPut("{restaurantId:guid}/tables/{tableId:guid}")]
        [Authorize(Policy = "TenantPolicy")]
        public async Task<IActionResult> UpdateTable(Guid restaurantId, Guid tableId,
            [FromBody] UpdateTableCommand command)
        {
            await _mediator.Send(command, HttpContext.RequestAborted);
            return Ok();
        }

        /// <summary>
        /// Removes a table from a restaurant.
        /// </summary>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <param name="tableId">Table Id</param>
        /// <returns>HTTP 200 OK on success.</returns>
        [HttpDelete("{restaurantId:guid}/tables/{tableId:guid}")]
        [Authorize(Policy = "TenantPolicy")]
        public async Task<IActionResult> RemoveTable(Guid restaurantId, Guid tableId)
        {
            var command = new RemoveTableCommand(restaurantId, tableId);
            await _mediator.Send(command, HttpContext.RequestAborted);
            return Ok();
        }
    }
}