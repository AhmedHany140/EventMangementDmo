using Domain.Enums;

namespace Domain.Entities
{
	public class Sponsor
	{
		public int Id { get; set; }
		public string Name { get; set; } //compant name
		public string? LogoUrl { get; set; }
		public string? Website { get; set; }
		public string? ContactPerson { get; set; } //مدير التسويق مثلا اسم
		public string? Email { get; set; }

		public SponsorLevel SponsorLevel { get; set; }
		public ICollection<EventSponsor>? EventSponsors { get; set; }

	}
}

