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


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
