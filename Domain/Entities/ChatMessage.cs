namespace Domain.Entities
{
	public class ChatMessage
	{
		public int Id { get; set; }

		public int SessionId { get; set; }
		public Session Session { get; set; }

		public string UserId { get; set; }
		public AppUser User { get; set; }

		public string Text { get; set; }
		public DateTime Timestamp { get; set; }

		public int? ParentMessageId { get; set; }
		public ChatMessage? ParentMessage { get; set; }
		public ICollection<ChatMessage>? Replies { get; set; }


	}

}

