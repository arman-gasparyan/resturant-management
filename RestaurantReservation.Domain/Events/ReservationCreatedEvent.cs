// Location: RestaurantReservation.Domain/DomainEvents/ReservationCreatedEvent.cs

using MediatR;
using RestaurantReservation.Domain.Models;

namespace RestaurantReservation.Domain.Events
{
    /// <summary>
    /// Event triggered when a new reservation is created.
    /// </summary>
    public class ReservationCreatedEvent : INotification
    {
        /// <summary>
        /// The reservation that was created.
        /// </summary>
        public Reservation Reservation { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReservationCreatedEvent"/> class.
        /// </summary>
        /// <param name="reservation">The reservation that was created.</param>
        public ReservationCreatedEvent(Reservation reservation)
        {
            Reservation = reservation ?? throw new ArgumentNullException(nameof(reservation));
        }
    }
}