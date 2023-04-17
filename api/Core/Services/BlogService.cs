using AutoMapper;
using Core.Interfaces;
using Data.Dtos;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Core.Services
{
	public class BlogService : IBlogService
	{
		private readonly BlogDbContext db;
		private readonly IMapper mapper;
    private readonly ILogger<BlogService> logger;

		public BlogService(BlogDbContext db, IMapper mapper, ILogger<BlogService> logger)
		{
      this.logger = logger;
			this.db = db;
			this.mapper = mapper;
		}
		public async Task<int> CreateAsync(BlogCreateEditDto entity)
		{
      logger.LogInformation(JsonSerializer.Serialize(entity));
			var result = await db.Blogs.AddAsync(mapper.Map<BlogCreateEditDto, Blog>(entity));
			return result.Entity.Id;
		}
		public async Task<IEnumerable<BlogDto>> GetAllAsync()
		{
			var obj = await db.Blogs
			.Include(o => o.Topic)
			.Include(o => o.User)
			.ToListAsync();
			return mapper.Map<IEnumerable<Blog>,IEnumerable<BlogDto>>(obj);
		}
		public async Task<BlogDto> GetByIdAsync(int id)
		{
			var obj = await db.Blogs
			.Include(o => o.Topic)
			.Include(o => o.User)
			.FirstOrDefaultAsync(u => u.Id == id);
			return mapper.Map<Blog, BlogDto>(obj);
		}
		public async Task<bool> RemoveAsync(int id)
		{
			var obj = await db.Blogs.FirstOrDefaultAsync(u => u.Id == id);
			db.Blogs.Remove(obj);
			return true;
		}
		public async Task<bool> UpdateAsync(BlogCreateEditDto entity)
		{
			var result = db.Blogs.Update(mapper.Map<BlogCreateEditDto, Blog>(entity));
			return true;
		}
		public async Task SaveAsync()
		{
			await db.SaveChangesAsync();
		}

	}
}
