using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class SessionSpeakerDetails
	{
		public int SessionId { get; set; }
		public int EventId { get; set; }
		public string Title { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int MaxParticipants { get; set; }
		public string? RecordingUrl { get; set; }
		public string? SessionLink { get; set; }
		public string SessionStatus { get; set; }
		public string SessionType { get; set; }
		public string? VirtualRoomId { get; set; }
		public string Bio { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string UserType { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime LastLogin { get; set; }
		public string Role { get; set; }
	}

}
