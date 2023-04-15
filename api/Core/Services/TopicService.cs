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

namespace Core.Services
{
	public class TopicService : ITopicService
	{
		private readonly BlogDbContext db;
		private readonly IMapper mapper;

		public TopicService(BlogDbContext db, IMapper mapper)
		{
			this.db = db;
			this.mapper = mapper;
		}
		public async Task<int> CreateAsync(TopicCreateEditDto entity)
		{
			var result = await db.Topics.AddAsync(mapper.Map<TopicCreateEditDto, Topic>(entity));
			return result.Entity.Id;
		}
		public async Task<IEnumerable<TopicDto>> GetAllAsync()
		{
			var obj = await db.Topics.ToListAsync();
			return mapper.Map<IEnumerable<Topic>, IEnumerable<TopicDto>>(obj);
		}
		public async Task<TopicDto> GetByIdAsync(int id)
		{
			var obj = await db.Topics.FirstOrDefaultAsync(u => u.Id == id);
			return mapper.Map<Topic, TopicDto>(obj);
		}
		public async Task<bool> RemoveAsync(int id)
		{
			var obj = await db.Topics.FirstOrDefaultAsync(u => u.Id == id);
			db.Topics.Remove(obj);
			return true;
		}
		public async Task<bool> UpdateAsync(TopicCreateEditDto entity)
		{
			var result = db.Topics.Update(mapper.Map<TopicCreateEditDto, Topic>(entity));
			return true;
		}
		public async Task SaveAsync()
		{
			await db.SaveChangesAsync();
		}
	}
}
