using Domain.Enums;

namespace Domain.Entities
{
	public class Event
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public EvenentStatus EvenentStatus { get; set; }
		public EventType EventType { get; set; }
		public int? MaxAttendees { get; set; }
		public bool IsRecurring { get; set; }
		public RecurrencePattern? RecurrencePattern { get; set; }
		public string? CoverImageUrl { get; set; }
		public string OrganizerId { get; set; }
		public AppUser Organizer { get; set; }
		public DateTime? RegistrationDeadline { get; set; }
		public string TimeZone { get; set; }
		public ICollection<Registrations>? Registrations { get; set; }

		public ICollection<TicketType>? TicketTypes { get; set; }

		public ICollection<Session>? Sessions { get; set; }

		public ICollection<EventSponsor>? EventSponsors { get; set; }

		public ICollection<Resource>? Resource { get; set; }

	}



}

