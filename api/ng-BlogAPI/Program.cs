using BlogApi.Endpoints;
using Core;
using Core.Interfaces;
using Core.Mapper;
using Core.Services;
using Microsoft.EntityFrameworkCore;
using userApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BlogDbContext>(options => 
	options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbConnection")!));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITopicService, TopicService>();

builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowOrigin", builder =>
  {
    builder.AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
  });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.ConfigureBlogEndpoints();
app.ConfigureTopicEndpoints();
app.ConfigureUserEndpoints();

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.Run();
