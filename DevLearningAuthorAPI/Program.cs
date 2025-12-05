using DevLearningAuthorAPI.Data;
using DevLearningAuthorAPI.Repository;
using DevLearningAuthorAPI.Repository.Interfaces;
using DevLearningAuthorAPI.Service;
using DevLearningAuthorAPI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDB"));

builder.Services.AddScoped<DbConnectionFactory>();

builder.Services.AddHttpClient("AuthorAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7242/api/course/");
});

builder.Services.AddScoped<AuthorRepository>();
builder.Services.AddScoped<AuthorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
