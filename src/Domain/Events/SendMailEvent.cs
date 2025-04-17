using ChatApp.Domain.Commons;

namespace ChatApp.Domain.Events
{
    public sealed class SendMailEvent : IDomainEvent
    {
        public required string To { get; init; }
        public required string Body { get; init; }
        public required string Subject { get; init; }
        public DateTimeOffset OccuredAt => DateTimeOffset.UtcNow;
        public Guid EventId => Guid.NewGuid();
    }
}
