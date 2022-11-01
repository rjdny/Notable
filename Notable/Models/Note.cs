using System;

namespace Notable.Models
{
	public class Note
	{
		public int Id { get; set; }
		public int UserProfileId { get; set; }
		public string Name { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public bool IsPublic { get; set; }

		public bool Belongs { get; set; }

		public UserProfile UserProfile { get; set; }
	}
}
