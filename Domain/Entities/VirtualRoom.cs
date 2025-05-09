namespace Domain.Entities
{
	public class VirtualRoom
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Platform { get; set; }
		public int MaxCapacity { get; set; }
		public string AccessCode { get; set; }
		public int SessionId { get; set; }
		public Session Session { get; set; }
	}
}

