namespace Domain.Entities
{
	public class TicketType
	{
		public int Id { get; set; }
		public int EventId { get; set; }
		public Event Event { get; set; }

		public int QuantityAvailable { get; set; }
		public decimal Price { get; set; }
		public string Name { get; set; }
		public DateTime SalesStartDate { get; set; }
		public DateTime SalesEndDate { get; set; }

		public ICollection<Registrations>? Registrations { get; set; }


	}
}

