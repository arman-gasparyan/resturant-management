using System.ComponentModel.DataAnnotations;
using MediatR;

namespace RestaurantReservation.Domain.Models
{
    public class Restaurant
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public TimeSpan DefaultReservationDuration { get; set; }
        public TimeSpan BufferTime { get; set; }
        public string TenantId { get; set; }
        
        [Timestamp]
        [ConcurrencyCheck]
        public byte[] RowVersion { get; private set; }
        
        public ICollection<Table> Tables { get; private set; } = new List<Table>();

        private readonly List<INotification> _domainEvents = new();
        
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

        public Restaurant(string name, TimeSpan openingTime, TimeSpan closingTime, TimeSpan defaultReservationDuration, TimeSpan bufferTime, string tenantId)
        {
            Name = name;
            OpeningTime = openingTime;
            ClosingTime = closingTime;
            DefaultReservationDuration = defaultReservationDuration;
            BufferTime = bufferTime;
            TenantId = tenantId;
        }

        public void AddTable(Table table)
        {
            Tables.Add(table);
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