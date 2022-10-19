using System;

namespace Notable.Models
{
	public class UserProfile
	{
		public int Id { get; set; }
		public string FirebaseUserId { get; set; }
		public string Username { get; set; }
		public string Email { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
