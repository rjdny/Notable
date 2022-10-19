using System;

namespace Notable.Models
{
	public class NoteComment
	{
		public int Id { get; set; }
		public int NoteId { get; set; }
		public int UserProfileId { get; set; }
        public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
