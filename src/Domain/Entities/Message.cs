namespace ChatApp.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public required int SenderId { get; set; }
        public required int ReceiverId { get; set; }
        public required bool IsSeen { get; set; }
        public required DateTimeOffset SentAt { get; set; }

        public virtual User Sender { get; set; } = null!;
        public virtual User Receiver { get; set; } = null!;
    }
}
