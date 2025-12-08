
using Models.Models;
using Models.Models.Dtos.Course;

namespace DevLearningCourseCategoryAPI.Repositories.Interfaces;

public interface ICourseRepository
{
	public Task<List<CourseResponseDto>> GetAllActivesCoursesAsync();
	public Task<CourseResponseDto?> GetCourseByIdAsync(Guid id);
	public Task CreateCourseAsync(Course course);
	public Task UpdateCourseAsync(Guid id, Course course);
	public Task DeleteCourseAsync(Guid id);
	public Task<AuthorCategoryDto> GetAuthorCategoryId(Guid id);
	public Task ActiveCourseAsync(Guid id);
    public Task<List<CourseResponseDto>> GetAllCoursesOrderedAsync();
}
