using System;

namespace Notable.Models
{
	public class Note
	{
		public int Id { get; set; }
		public int UserProfileId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
