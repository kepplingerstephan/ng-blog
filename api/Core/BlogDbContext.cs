using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
	public class BlogDbContext : DbContext
	{
		public BlogDbContext(DbContextOptions<BlogDbContext> options):base(options) { }
		
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Topic> Topics{ get; set; }
	}
}
