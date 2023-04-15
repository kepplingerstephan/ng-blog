using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Dtos
{
	public class TopicDto : TopicCreateEditDto
	{
	}
	public class TopicCreateEditDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<Blog> Blogs { get; set; }
	}
}
