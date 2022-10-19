namespace Notable.Models
{
	public class CategoryNote
	{
		public int Id { get; set; }
		public int CategoryId { get; set; }
		public int NoteId { get; set; }
		public int UserProfileId { get; set; }
	}
}
