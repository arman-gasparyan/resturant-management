using MediatR;
using Microsoft.Extensions.Logging;
using RestaurantReservation.Application.CQRS.Commands;
using RestaurantReservation.Application.Services;
using RestaurantReservation.Domain.Repositories;

namespace RestaurantReservation.Application.CQRS.CommandHandlers
{
    /// <summary>
    /// Handler for creating a new restaurant.
    /// </summary>
    public class CreateRestaurantCommandHandler : IRequestHandler<CreateRestaurantCommand, Guid>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger;
        private readonly IUserContextService _userContextService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRestaurantCommandHandler"/> class.
        /// </summary>
        /// <param name="restaurantRepository">Repository for restaurants.</param>
        /// <param name="mediator">Mediator for publishing domain events.</param>
        /// <param name="logger">Logger instance.</param>
        /// <param name="userContextService">Service to retrieve user context.</param>
        public CreateRestaurantCommandHandler(
            IRestaurantRepository restaurantRepository,
            IMediator mediator,
            ILogger<CreateRestaurantCommandHandler> logger,
            IUserContextService userContextService)
        {
            _restaurantRepository = restaurantRepository;
            _mediator = mediator;
            _logger = logger;
            _userContextService = userContextService;
        }

        /// <summary>
        /// Handles the creation of a new restaurant.
        /// </summary>
        /// <param name="request">The command containing restaurant details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>The unique identifier of the newly created restaurant.</returns>
        public async Task<Guid> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var restaurant = new Domain.Models.Restaurant(
                request.Name,
                request.OpeningTime,
                request.ClosingTime,
                request.DefaultReservationDuration,
                request.BufferTime,
                _userContextService.TenantId
            );

            await _restaurantRepository.AddAsync(restaurant, cancellationToken);

            await _restaurantRepository.SaveChangesAsync(cancellationToken);

            foreach (var domainEvent in restaurant.DomainEvents)
            {
                await _mediator.Publish(domainEvent, cancellationToken);
            }

            restaurant.ClearDomainEvents();

            return restaurant.Id;
        }
    }
}