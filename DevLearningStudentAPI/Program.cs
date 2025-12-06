using DevLearningStudentAPI.Data;
using DevLearningStudentAPI.Repositories;
using DevLearningStudentAPI.Repositories.Interfaces;
using DevLearningStudentAPI.Services;
using DevLearningStudentAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddSingleton<StudentRepository>();
builder.Services.AddSingleton<StudentCourseRepository>();
builder.Services.AddSingleton<StudentService>();
builder.Services.AddSingleton<StudentCourseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
