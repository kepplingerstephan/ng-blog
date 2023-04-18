namespace Data.Models
{
	public class Blog
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public User User { get; set; }
		public int TopicId { get; set; }
		public Topic Topic { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public string Content { get; set; }
		public string Title {get;set;}
	}
}