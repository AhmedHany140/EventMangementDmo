namespace Domain.Entities
{
	public class SessionSpeaker
	{
		public int SessionId { get; set; }
		public Session Session { get; set; }
		public string SpeakerId { get; set; }
		public AppUser Speaker { get; set; }
		public string Role { get; set; }
	}
}

