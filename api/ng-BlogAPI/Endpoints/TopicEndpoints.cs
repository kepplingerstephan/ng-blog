using AutoMapper;
using Core.Interfaces;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BlogApi.Endpoints
{
	public static class TopicEndpoints
	{

		public static void ConfigureTopicEndpoints(this WebApplication app)
		{
			app.MapGet("/api/topic", GetAllTopics)
					.WithName("GetTopics")
					.Produces<APIResponse>(200);
			app.MapGet("/api/topic/{id:int}", GetTopic)
				.WithName("GetTopic")
				.Produces<APIResponse>(200);

			app.MapPost("/api/topic", CreateTopic)
				.WithName("CreateTopic")
				.Accepts<TopicCreateEditDto>("application/json")
				.Produces<APIResponse>(201)
				.Produces(400);

			app.MapPut("/api/topic", UpdateTopic)
					.WithName("UpdateTopic")
					.Accepts<TopicCreateEditDto>("application/json")
					.Produces<APIResponse>(200)
					.Produces(400);

			app.MapDelete("/api/topic/{id:int}", DeleteTopic);
		}
		private async static Task<IResult> GetAllTopics(ITopicService service, ILogger<Program> logger)
		{
			APIResponse response = new();
			logger.Log(LogLevel.Information, "Getting all Topics");
			response.Result = await service.GetAllAsync();
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.OK;
			return Results.Ok(response);
		}
		private static async Task<IResult> GetTopic(ITopicService service, ILogger<Program> _logger, int id)
		{
			APIResponse response = new();
			response.Result = await service.GetByIdAsync(id);
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.OK;
			return Results.Ok(response);
		}

		private static async Task<IResult> UpdateTopic(ITopicService service, IMapper mapper, [FromBody] TopicCreateEditDto blogDto)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			bool success = await service.UpdateAsync(blogDto);
			await service.SaveAsync();

			response.Result = mapper.Map<TopicDto>(await service.GetByIdAsync(blogDto.Id)); ;
			response.IsSuccess = success;
			if (success)
				response.StatusCode = HttpStatusCode.OK;
			else
				response.StatusCode = HttpStatusCode.BadRequest;
			return Results.Ok(response);
		}

		private static async Task<IResult> CreateTopic(ITopicService service, IMapper mapper, [FromBody] TopicCreateEditDto blogDto)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			int resultId = await service.CreateAsync(blogDto);
			await service.SaveAsync();

			TopicDto blogResultDto = mapper.Map<TopicDto>(await service.GetByIdAsync(resultId));
			response.Result = blogResultDto;
			response.IsSuccess = true;
			response.StatusCode = HttpStatusCode.Created;
			return Results.Ok(response);
		}

		private static async Task<IResult> DeleteTopic(ITopicService service, int id)
		{
			APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

			TopicDto blog = await service.GetByIdAsync(id);
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
