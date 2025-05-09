using Domain.Enums;

namespace Domain.Entities
{
	public class EventSponsor
	{
		public int EventId { get; set; }
		public Event Event { get; set; }
		public int SponsorId { get; set; }
		public Sponsor Sponsor { get; set; }
		public double Amount { get; set; }
		public SponsorLevel SponsorshipLevel { get; set; } 

	}
}

