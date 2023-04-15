using AutoMapper;
using BlogApi.Endpoints;
using Core.Interfaces;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace userApi.Endpoints
{
	public static class UserEndpoints
	{
		public static void ConfigureUserEndpoints(this WebApplication app)
		{
			app.MapGet("/api/user", GetAllUsers)
					.WithName("GetUsers")
					.Produces<APIResponse>(200);
			app.MapGet("/api/user/{id:int}", GetUser)
				.WithName("GetUser")
				.Produces<APIResponse>(200);

			app.MapPost("/api/user", CreateUser)
				.WithName("CreateUser")
				.Accepts<UserCreateEditDto>("application/json")
				.Produces<APIResponse>(201)
				.Produces(400);

			app.MapPut("/api/user", UpdateUser)
					.WithName("UpdateUser")
					.Accepts<UserCreateEditDto>("application/json")
					.Produces<APIResponse>(200)
					.Produces(400);

			app.MapDelete("/api/user/{id:int}", DeleteUser);
		}
		private async static Task<IResult> GetAllUsers(IUserService service, ILogger<Program> logger)
		{
			APIResponse response = new();
			logger.Log(LogLevel.Information, "Getting all Users");
			response.Result = await service.GetAllAsync();
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.OK;
			return Results.Ok(response);
		}
		private static async Task<IResult> GetUser(IUserService service, ILogger<Program> _logger, int id)
		{
			APIResponse response = new();
			response.Result = await service.GetByIdAsync(id);
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.OK;
			return Results.Ok(response);
		}

		private static async Task<IResult> UpdateUser(IUserService service, IMapper mapper, [FromBody] UserCreateEditDto blogDto)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			bool success = await service.UpdateAsync(blogDto);
			await service.SaveAsync();

			response.Result = mapper.Map<UserDto>(await service.GetByIdAsync(blogDto.Id)); ;
			response.IsSuccess = success;
			if (success)
				response.StatusCode = HttpStatusCode.OK;
			else
				response.StatusCode = HttpStatusCode.BadRequest;
			return Results.Ok(response);
		}

		private static async Task<IResult> CreateUser(IUserService service, IMapper mapper, [FromBody] UserCreateEditDto blogDto)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			int resultId = await service.CreateAsync(blogDto);
			await service.SaveAsync();

			UserDto blogResultDto = mapper.Map<UserDto>(await service.GetByIdAsync(resultId));
			response.Result = blogResultDto;
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.Created;
			return Results.Ok(response);
		}

		private static async Task<IResult> DeleteUser(IUserService service, int id)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			UserDto blog = await service.GetByIdAsync(id);
			if (blog != null)
			{
				await service.RemoveAsync(blog.Id);
				await service.SaveAsync();
				response.IsSuccess = true;
				response.StatusCode = HttpStatusCode.NoContent;
				return Results.Ok(response);
			}
			else
			{
				response.ErrorMessages.Add("Invalid Id");
				return Results.BadRequest(response);
			}
		}
	}
}
