using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Application.CQRS.Commands;
using RestaurantReservation.Domain.Exceptions;
using RestaurantReservation.Domain.Models;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.CommandHandlers
{
    /// <summary>
    /// Handler for creating a new reservation.
    /// </summary>
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateReservationCommandHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateReservationCommandHandler"/> class.
        /// </summary>
        /// <param name="reservationRepository">Repository for reservations.</param>
        /// <param name="tableRepository">Repository for tables.</param>
        /// <param name="restaurantRepository">Repository for restaurants.</param>
        /// <param name="mediator">Mediator for publishing domain events.</param>
        /// <param name="logger">Logger instance.</param>
        public CreateReservationCommandHandler(
            IReservationRepository reservationRepository,
            ITableRepository tableRepository,
            IRestaurantRepository restaurantRepository,
            IMediator mediator,
            ILogger<CreateReservationCommandHandler> logger)
        {
            _reservationRepository = reservationRepository;
            _tableRepository = tableRepository;
            _restaurantRepository = restaurantRepository;
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Handles the creation of a new reservation.
        /// </summary>
        /// <param name="request">The command containing reservation details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The unique identifier of the newly created reservation.</returns>
        public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetByIdAsync(request.TableId, cancellationToken)
                        ?? throw new NotFoundException("Table not found.");

            var restaurant = await _restaurantRepository.GetByIdAsync(table.RestaurantId, cancellationToken)
                             ?? throw new NotFoundException("Restaurant not found.");

            var duration = request.Duration > TimeSpan.Zero
                ? request.Duration
                : restaurant.DefaultReservationDuration;
            
            var desiredStartTimeUtc = request.ReservationTime.ToUniversalTime();
            
            var desiredEndTimeUtc = desiredStartTimeUtc
                .Add(duration)
                .Add(restaurant.BufferTime);

            bool hasOverlap = await _reservationRepository.HasOverlappingReservationAsync(
                table.Id,
                desiredStartTimeUtc,
                desiredEndTimeUtc,
                cancellationToken);

            if (hasOverlap)
            {
                throw new InvalidOperationException("Table is not available at the selected time.");
            }

            var reservation = new Reservation(
                Guid.NewGuid(),
                request.ReservationTime,
                request.Email,
                request.NumberOfVisitors,
                table.Id,
                duration
            );

           await _reservationRepository.AddAsync(reservation, cancellationToken);

            try
            {
                await _reservationRepository.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "Concurrency conflict occurred while creating a reservation.");
                throw new InvalidOperationException("The reservation could not be completed due to a conflict. Please try again.");
            }

            return reservation.Id;
        }

    }
}