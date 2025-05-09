using Domain.Enums;

namespace Domain.Entities
{
	public class Registrations
	{
		public int Id { get; set; }
		public string AttendanceId { get; set; }
		public AppUser Attendance { get; set; }
		public int EventId { get; set; }
		public Event Event { get; set; }
		public int TicketTypeId { get; set; }
		public TicketType TicketType { get; set; }
		public DateTime RegistrationDate { get; set; }
		public CheckInStatus CheckInStatus { get; set; }
		public DateTime ChickInTime { get; set; }

	}




}

