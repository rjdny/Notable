namespace Notable.Models
{
	public class NoteLike
	{
		public int Id { get; set; }
		public int NoteId { get; set; }
		public int UserProfileId { get; set; }

		//public UserProfile Owner { get; set; }
		//public Note Note { get; set; }
	}
}
