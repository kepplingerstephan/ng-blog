using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
	public class BlogDto : BlogCreateEditDto
	{
		public User User { get; set; }
		public Topic Topic { get; set; }
	}
	public class BlogCreateEditDto
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int TopicId { get; set; }
		public DateTime Created { get; set; }
		public DateTime Updated { get; set; }
		public string Content { get; set; }
    public string Title { get; set; }
	}
}
