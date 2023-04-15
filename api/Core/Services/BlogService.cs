using AutoMapper;
using Core.Interfaces;
using Data.Dtos;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Core.Services
{
	public class BlogService : IBlogService
	{
		private readonly BlogDbContext db;
		private readonly IMapper mapper;

		public BlogService(BlogDbContext db, IMapper mapper)
		{
			this.db = db;
			this.mapper = mapper;
		}
		public async Task<int> CreateAsync(BlogCreateEditDto entity)
		{
			var result = await db.Blogs.AddAsync(mapper.Map<BlogCreateEditDto, Blog>(entity));
			return result.Entity.Id;
		}
		public async Task<IEnumerable<BlogDto>> GetAllAsync()
		{
			var obj = await db.Blogs.ToListAsync();
			return mapper.Map<IEnumerable<Blog>,IEnumerable<BlogDto>>(obj);
		}
		public async Task<BlogDto> GetByIdAsync(int id)
		{
			var obj = await db.Blogs.FirstOrDefaultAsync(u => u.Id == id);
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
