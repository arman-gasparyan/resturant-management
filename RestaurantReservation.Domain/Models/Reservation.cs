using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using RestaurantReservation.Domain.Events;

namespace RestaurantReservation.Domain.Models
{
    public class Reservation
    {
        public Guid Id { get; private set; }
        public DateTime ReservationTime { get; private set; }
        public string Email { get; private set; }
        public int NumberOfVisitors { get; private set; }
        public Guid TableId { get; private set; }
        public Table Table { get; private set; }

        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; private set; }

        public TimeSpan Duration { get; private set; }

        [NotMapped]
        public DateTime EndTime => ReservationTime.Add(Duration);

        private readonly List<INotification> _domainEvents = new();
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        private Reservation() { }

        public Reservation(Guid id, DateTime reservationTime, string email, int numberOfVisitors, Guid tableId, TimeSpan duration)
        {
            Id = id;
            ReservationTime = reservationTime;
            Email = email;
            NumberOfVisitors = numberOfVisitors;
            TableId = tableId;
            Duration = duration;
            
            var reservationCreatedEvent = new ReservationCreatedEvent(this);
            
            _domainEvents.Add(reservationCreatedEvent);
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }
}