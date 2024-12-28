using Microsoft.AspNetCore.Mvc;
using MediatR;
using RestaurantReservation.Application.Services;
using RestaurantReservation.Application.CQRS.Commands;

namespace RestaurantReservation.Web.Api.Controllers
{
    [ApiController]
    [Route("api/reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationsController"/> class.
        /// </summary>
        /// <param name="mediator">The MediatR mediator instance.</param>
        /// <param name="userContextService">Service to retrieve user context.</param>
        public ReservationsController(IMediator mediator, IUserContextService userContextService)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new reservation for a table.
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="reservationTime"></param>
        /// <param name="email"></param>
        /// <param name="numberOfVisitors"></param>
        /// <param name="duration"></param>
        /// <returns>Newly created Reservation Id</returns>
        [HttpPost]
        public async Task<IActionResult> CreateReservation(
            Guid tableId,
            DateTime reservationTime,
            string email,
            int numberOfVisitors,
            TimeSpan duration)
        {
            var reservationId = await _mediator.Send(
                    new CreateReservationCommand(tableId, reservationTime, email, numberOfVisitors, duration), HttpContext.RequestAborted);

            return CreatedAtAction(nameof(CreateReservation),
                new { id = reservationId }, new { ReservationId = reservationId });
        }

        /// <summary>
        /// Cancels (removes) a reservation by Id.
        /// </summary>
        /// <param name="id">Reservation Id</param>
        /// <returns>HTTP 200 OK on success.</returns>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> CancelReservation(Guid id)
        {
            var command = new CancelReservationCommand(id);

            await _mediator.Send(command, HttpContext.RequestAborted);

            return Ok();
        }
    }
}