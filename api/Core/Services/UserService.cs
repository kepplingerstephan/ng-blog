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
	public class UserService : IUserService
	{
		private readonly BlogDbContext db;
		private readonly IMapper mapper;

		public UserService(BlogDbContext db, IMapper mapper)
		{
			this.db = db;
			this.mapper = mapper;
		}
		public async Task<int> CreateAsync(UserCreateEditDto entity)
		{
			var result = await db.Users.AddAsync(mapper.Map<UserCreateEditDto, User>(entity));
			return result.Entity.Id;
		}
		public async Task<IEnumerable<UserDto>> GetAllAsync()
		{
			var obj = await db.Users.ToListAsync();
			return mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(obj);
		}
		public async Task<UserDto> GetByIdAsync(int id)
		{
			var obj = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
			return mapper.Map<User, UserDto>(obj);
		}
		public async Task<bool> RemoveAsync(int id)
		{
			var obj = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
			db.Users.Remove(obj);
			return true;
		}
		public async Task<bool> UpdateAsync(UserCreateEditDto entity)
		{
			var result = db.Users.Update(mapper.Map<UserCreateEditDto, User>(entity));
			return true;
		}
		public async Task SaveAsync()
		{
			await db.SaveChangesAsync();
		}
	}
}
