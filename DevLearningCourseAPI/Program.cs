using DevLearningCourseCategoryAPI.Data;
using DevLearningCourseCategoryAPI.Repositories;
using DevLearningCourseCategoryAPI.Repositories.Interfaces;
using DevLearningCourseCategoryAPI.Services;
using DevLearningCourseCategoryAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<DbConnectionFactory>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


var app = builder.Build();


// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
