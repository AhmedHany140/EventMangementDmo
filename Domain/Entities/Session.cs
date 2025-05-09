using Domain.Enums;

namespace Domain.Entities
{
	public class Session
	{
		public int Id { get; set; }
		public int EventId { get; set; }
		public Event Event { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int MaxParticipants { get; set; }
		public string? RecordingUrl { get; set; }
		public string? SessionLink { get; set; }
		public SessionStatus SessionStatus { get; set; }
		public SessionType SessionType { get; set; }
		public int? VirtualRoomId { get; set; }
		public VirtualRoom? VirtualRoom { get; set; }
		public ICollection<Resource>? Resources { get; set; }
		public ICollection<Poll>? Polls { get; set; }
	
		public ICollection<ChatMessage>? ChatMessages { get; set; }
		public ICollection<SessionSpeaker>? SessionSpeakers { get; set; }

	}






}

