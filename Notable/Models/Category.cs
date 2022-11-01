namespace Notable.Models
{
	public class Category
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int UserProfileId { get; set; }

		//public UserProfile Creator { get; set; }

	}
}
