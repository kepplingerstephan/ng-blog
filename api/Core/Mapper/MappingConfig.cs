using AutoMapper;
using Data.Dtos;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapper
{
	public class MappingConfig :Profile
	{
		public MappingConfig() 
		{
			CreateMap<Blog, BlogDto>().ReverseMap();
			CreateMap<Blog, BlogCreateEditDto>().ReverseMap();

			CreateMap<User, UserDto>().ReverseMap();
			CreateMap<User, UserCreateEditDto>().ReverseMap();
		
			CreateMap<Topic, TopicDto>().ReverseMap();
			CreateMap<Topic, TopicCreateEditDto>().ReverseMap();
		}
	}
}
