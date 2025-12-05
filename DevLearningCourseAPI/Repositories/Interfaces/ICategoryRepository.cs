
using Models.Models;
using Models.Models.Dtos.Category;

namespace DevLearningCourseCategoryAPI.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<List<CategoryResponseDto>> GetAllCategoriesAsync();
    Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id);
    Task CreateCategoryAsync(Category category);
    Task UpdateCategoryAsync(Guid id, Category category);
    Task DeleteCategoryAsync(Guid id);
}
