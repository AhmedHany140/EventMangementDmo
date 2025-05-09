using Domain.Enums;

namespace Domain.Entities
{
	public class Resource
	{
		public int Id { get; set; }
		public int SessionId { get; set; }
		public Session Session { get; set; }
		public int EventId { get; set; }
		public Event Event { get; set; }
		public string Name { get; set; }
		public string Type  { get; set; }
		public string Url { get; set; }
		public DateTime UploadDate { get; set; }
		public AccessLevel AccessLevel { get; set; }

	}




}

