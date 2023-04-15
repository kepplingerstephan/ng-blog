using AutoMapper;
using Core.Interfaces;
using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Net;
using static Azure.Core.HttpHeader;

namespace BlogApi.Endpoints
{
	public static class BlogEndpoints
	{
		public static void ConfigureBlogEndpoints(this WebApplication app)
		{
			app.MapGet("/api/blog", GetAllBlogs)
				.WithName("GetBlogs")
				.Produces<APIResponse>(200);
			app.MapGet("/api/blog/{id:int}", GetBlog)
				.WithName("GetBlog")
				.Produces<APIResponse>(200);

			app.MapPost("/api/blog", CreateBlog)
				.WithName("CreateBlog")
				.Accepts<BlogCreateEditDto>("application/json")
				.Produces<APIResponse>(201)
				.Produces(400);

			app.MapPut("/api/blog", UpdateBlog)
				.WithName("UpdateBlog")
				.Accepts<BlogCreateEditDto>("application/json")
				.Produces<APIResponse>(200)
				.Produces(400);

			app.MapDelete("/api/blog/{id:int}", DeleteBlog);

		}
		private async static Task<IResult> GetAllBlogs(IBlogService service, ILogger<Program> logger)
		{
			APIResponse response = new();
			logger.Log(LogLevel.Information, "Getting all Blogs");
			response.Result = await service.GetAllAsync();
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.OK;
			return Results.Ok(response);
		}
		private static async Task<IResult> GetBlog(IBlogService service, ILogger<Program> _logger, int id)
		{
			APIResponse response = new();
			response.Result = await service.GetByIdAsync(id);
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.OK;
			return Results.Ok(response);
		}

		private static async Task<IResult> UpdateBlog(IBlogService service, IMapper mapper, [FromBody] BlogCreateEditDto blogDto)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			bool success = await service.UpdateAsync(blogDto);
			await service.SaveAsync();

			response.Result = mapper.Map<BlogDto>(await service.GetByIdAsync(blogDto.Id)); ;
			response.IsSuccess = success;
			if (success)
				response.StatusCode = HttpStatusCode.OK;
			else
				response.StatusCode = HttpStatusCode.BadRequest;
			return Results.Ok(response);
		}

		private static async Task<IResult> CreateBlog(IBlogService service, IMapper mapper, [FromBody] BlogCreateEditDto blogDto)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			int resultId = await service.CreateAsync(blogDto);
			await service.SaveAsync();

			BlogDto blogResultDto = mapper.Map<BlogDto>(await service.GetByIdAsync(resultId));
			response.Result = blogResultDto;
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.Created;
			return Results.Ok(response);
		}

		private static async Task<IResult> DeleteBlog(IBlogService service, int id)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			BlogDto blog = await service.GetByIdAsync(id);
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
