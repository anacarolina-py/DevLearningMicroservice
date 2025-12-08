using DevLearningCareerAPI.Data;
using DevLearningCareerAPI.Repositories;
using DevLearningCareerAPI.Repositories.Interfaces;
using DevLearningCareerAPI.Services;
using DevLearningCareerAPI.Services.Interfaces;

using System.Data.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<DbConnectionFactory>();

builder.Services.AddScoped<ICareerService, CareerService>();
builder.Services.AddScoped<ICareerRepository, CareerRepository>();
builder.Services.AddScoped<ICareerItemService, CareerItemService>();
builder.Services.AddScoped<ICareerItemRepository, CareerItemRepository>();

builder.Services.AddHttpClient<ICareerItemService, CareerItemService>(client => client.BaseAddress = new Uri("https://localhost:7242/api/courses/"));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
