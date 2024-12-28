using MediatR;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Domain.Events;
using RestaurantReservation.Domain.Services;

namespace RestaurantReservation.Domain.EventHandlers
{
    /// <summary>
    /// Handles the ReservationCreatedEvent by sending a confirmation email.
    /// </summary>
    public class ReservationCreatedEventHandler : INotificationHandler<ReservationCreatedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ReservationCreatedEventHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationCreatedEventHandler"/> class.
        /// </summary>
        /// <param name="emailService">Service to send emails.</param>
        /// <param name="logger">Logger instance.</param>
        public ReservationCreatedEventHandler(IEmailService emailService, ILogger<ReservationCreatedEventHandler> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        /// <summary>
        /// Handles the ReservationCreatedEvent by sending a confirmation email.
        /// </summary>
        /// <param name="notification">The ReservationCreatedEvent instance.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task Handle(ReservationCreatedEvent notification, CancellationToken cancellationToken)
        {
            var reservation = notification.Reservation;

            // Prepare email content
            var subject = "Your Reservation Confirmation";
            
            var body = $"Dear Customer,\n\nThank you for your reservation at our restaurant.\n\n" +
                       $"Reservation Details:\n" +
                       $"- Reservation ID: {reservation.Id}\n" +
                       $"- Table ID: {reservation.TableId}\n" +
                       $"- Date and Time: {reservation.ReservationTime}\n" +
                       $"- Number of Guests: {reservation.NumberOfVisitors}\n\n" +
                       $"We look forward to serving you!\n\nBest Regards,\nRestaurant Team";

            try
            {
                await _emailService.SendEmailAsync(reservation.Email, subject, body);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Failed to send confirmation email to {reservation.Email} for reservation {reservation.Id}.");
            }
        }
    }
}
