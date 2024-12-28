using MediatR;

namespace RestaurantReservation.Application.CQRS.Commands
{
    /// <summary>
    /// Command to create a new restaurant.
    /// </summary>
    public class CreateRestaurantCommand : IRequest<Guid>
    {
        /// <summary>
        /// The name of the restaurant.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The opening time of the restaurant.
        /// </summary>
        public TimeSpan OpeningTime { get; set; }

        /// <summary>
        /// The closing time of the restaurant.
        /// </summary>
        public TimeSpan ClosingTime { get; set; }

        /// <summary>
        /// The default duration for reservations.
        /// </summary>
        public TimeSpan DefaultReservationDuration { get; set; }

        /// <summary>
        /// The buffer time between reservations.
        /// </summary>
        public TimeSpan BufferTime { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRestaurantCommand"/> class.
        /// </summary>
        /// <param name="name">Name of the restaurant.</param>
        /// <param name="openingTime">Opening time.</param>
        /// <param name="closingTime">Closing time.</param>
        /// <param name="defaultReservationDuration">Default reservation duration.</param>
        /// <param name="bufferTime">Buffer time between reservations.</param>
        public CreateRestaurantCommand(string name, TimeSpan openingTime, TimeSpan closingTime, TimeSpan defaultReservationDuration, TimeSpan bufferTime)
        {
            Name = name;
            OpeningTime = openingTime;
            ClosingTime = closingTime;
            DefaultReservationDuration = defaultReservationDuration;
            BufferTime = bufferTime;
        }
    }
}