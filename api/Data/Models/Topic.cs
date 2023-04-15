namespace Data.Models
{
	public class Topic
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Blog> Blogs { get; set; }
	}
}