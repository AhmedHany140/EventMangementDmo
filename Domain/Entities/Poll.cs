using Domain.Enums;

namespace Domain.Entities
{
	public class Poll
	{
		public int Id { get; set; }
		public int SessionId { get; set; }
		public Session Session { get; set; }
		public string Question { get; set; }

		public bool IsAnonymous { get; set; }
		public DateTime CreatedAt { get; set; }

		public PollType PollType { get; set; }

	}




}

