using DevLearningCourseCategoryAPI.Controllers;
using DevLearningCourseCategoryAPI.Data;
using DevLearningCourseCategoryAPI.Repositories;
using DevLearningCourseCategoryAPI.Repositories.Interfaces;
using DevLearningCourseCategoryAPI.Services;
using DevLearningCourseCategoryAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<DbConnectionFactory>();

builder.Services.AddSingleton<ICourseRepository, CourseRepository>();
builder.Services.AddSingleton<CategoryRepository>();    

builder.Services.AddSingleton<ICourseService, CourseService>();
builder.Services.AddSingleton<CategoryService>();

builder.Services.AddHttpClient<ICourseService, CourseService>(client => client.BaseAddress = new Uri("https://localhost:7037/api/Author/"));
builder.Services.AddHttpClient<ICourseRepository, CourseRepository>(client => client.BaseAddress = new Uri("https://localhost:7037/api/Author/"));


var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
