using Models.Models;
using Models.Models.Dtos.Category;
using DevLearningCourseCategoryAPI.Repositories;
using DevLearningCourseCategoryAPI.Repositories.Interfaces;
using DevLearningCourseCategoryAPI.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Models.Models.Dtos.Category;

namespace DevLearningCourseCategoryAPI.Services;

public class CategoryService : ICategoryService
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryResponseDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        return categories.OrderBy(x => x.Title).ToList();
    }

    public async Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id)
    {
        return await _categoryRepository.GetCategoryByIdAsync(id);
    }

    public async Task CreateCategoryAsync(CreateCategoryDto category)
    {
        var newCategory = new Category(category.Title,
                                       category.Url,
                                       category.Summary,
                                       category.Order,
                                       category.Description,
                                       category.Featured);

        await _categoryRepository.CreateCategoryAsync(newCategory);
    }

    public async Task UpdateCategoryAsync(Guid id, UpdateCategoryDto categoryRequest)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);

        var newCategory = new Category(

            string.IsNullOrEmpty(categoryRequest.Title)
                               ? category.Title
                               : categoryRequest.Title,
            string.IsNullOrEmpty(categoryRequest.Url)
                               ? category.Url
                               : categoryRequest.Url,
            string.IsNullOrEmpty(categoryRequest.Summary)
                               ? category.Summary
                               : categoryRequest.Summary,
                                 categoryRequest.Order is 0
                               ? category.Order
                               : categoryRequest.Order,
            string.IsNullOrEmpty(categoryRequest.Description)
                               ? category.Description
                               : categoryRequest.Description,

                                 categoryRequest.Featured
                                      );

        await _categoryRepository.UpdateCategoryAsync(id, newCategory);
    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        if(await _categoryRepository.HasCourseAsync(id))
        {
            throw new ArgumentException("Não é possivel deletar um acetgoria que possui cursos associados");
        }

        await _categoryRepository.DeleteCategoryAsync(id);
    }
}
