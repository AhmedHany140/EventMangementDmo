using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class EventSponsorDetails
	{
		public int EventId { get; set; }
		public string EventTitle { get; set; }
		public string Description { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int SponsorId { get; set; }
		public string SponsorName { get; set; }
		public string ContactPerson { get; set; }
		public string Email { get; set; }
		public double Amount { get; set; }
		public string SponsorshipLevel { get; set; }
	}


}
