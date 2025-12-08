using DevLearningCourseCategoryAPI.Repositories.Interfaces;
using DevLearningCourseCategoryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Models.Dtos.Author;
using Models.Models.Dtos.Course;

namespace DevLearningCourseCategoryAPI.Services;

public class CourseService : ICourseService
{
	private readonly ICourseRepository _repository;
	private readonly HttpClient _httpClienteAuthor;

	public CourseService(ICourseRepository repository, HttpClient httpAuthor)
	{
		_repository = repository;
		_httpClienteAuthor = httpAuthor;
	}

	public async Task<List<CourseResponseDto>> GetAllCoursesAsync()
	{
        return await _repository.GetAllActivesCoursesAsync();
	}

	public async Task<CourseResponseDto?> GetCourseByIdAsync(Guid id)
	{
		return await _repository.GetCourseByIdAsync(id);
	}

	public async Task<List<CourseResponseDto>> GetAllCoursesOrderedAsync()
	{
		return await _repository.GetAllCoursesOrderedAsync();
	}

	public async Task CreateCourseAsync(CreateCourseDto courseDto)
	{
		var author = await _httpClienteAuthor.GetFromJsonAsync<AuthorResponseDto>(courseDto.AuthorId.ToString());

		if(author is null)
		{
			throw new Exception("Author not found!");
		}

		var course = new Course(
			courseDto.Tag,
			courseDto.Title,
			courseDto.Summary,
			courseDto.Url,
			courseDto.Level,
			courseDto.DurationInMinutes,
			true,
			courseDto.Free,
			courseDto.Featured,
			courseDto.AuthorId,
			courseDto.CategoryId,
			courseDto.Tags
			);

		await _repository.CreateCourseAsync(course);
	}

	public async Task ActiveCourseAsync(Guid id)
	{
		await _repository.ActiveCourseAsync(id);
	}

	public async Task UpdateCourseAsync(Guid id, UpdateCourseDto courseDto)
	{
		var courseResponse = await _repository.GetCourseByIdAsync(id);
		var authorCategory = await _repository.GetAuthorCategoryId(id);

		var course = new Course(
			string.IsNullOrEmpty(courseDto.Tag)
								? courseResponse.Tag
								: courseDto.Tag,
			string.IsNullOrEmpty(courseDto.Title)
								? courseResponse.Title
								: courseDto.Title,
			string.IsNullOrEmpty(courseDto.Summary)
								? courseResponse.Summary
								: courseDto.Summary,
			string.IsNullOrEmpty(courseDto.Url)
								? courseResponse.Url
								: courseDto.Url,
			courseDto.Level is 0
					  ? courseResponse.Level
					  : courseDto.Level,
			courseDto.DurationInMinutes is 0
					  ? courseResponse.DurationInMinutes
					  : courseDto.DurationInMinutes,
			courseDto.Active,
			courseDto.Free,
			courseDto.Featured,
			string.IsNullOrEmpty(courseDto.AuthorId)
					  ? authorCategory.AuthorId
					  : courseDto.AuthorId,
			courseDto.CategoryId == Guid.Empty
					  ? authorCategory.CategoryId
					  : courseDto.CategoryId,
			string.IsNullOrEmpty(courseDto.Tags)
								? courseResponse.Tags
								: courseDto.Tags
			);

		await _repository.UpdateCourseAsync(id, course);
	}

    public async Task<bool> SelectCourseByStudentAsync(Guid courseId)
	{
        var course = await _repository.SelectCourseByStudentAsync(courseId);

        if (course.Quantidade > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public async Task DeleteCourseAsync(Guid id)
	{
		await _repository.DeleteCourseAsync(id);
	}
}
