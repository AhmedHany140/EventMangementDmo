using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class AppUser : IdentityUser
	{
		public string Bio { get; set; }
		public string? Picture { get; set; }
		public UserType UserType { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime LastLogin { get; set; }
		public ICollection<Registrations> Registrations { get; set; }
		public ICollection<Event> Events { get; set; }
		public ICollection<ChatMessage>? ChatMessages { get; set; }
		public ICollection<SessionSpeaker> SessionSpeakers { get; set; }

		public string RefreshToken { get; set; }
		public DateTime RefreshTokenExpiryTime { get; set; }
	}

}

