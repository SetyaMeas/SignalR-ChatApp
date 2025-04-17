using CQRS;

namespace ChatApp.Domain.Commons
{
    public interface IDomainEvent : IEvent
    {
        DateTimeOffset OccuredAt { get; }
        Guid EventId { get; }
    }
}
