using Microsoft.AspNetCore.Mvc;
using Models.Models.Dtos.CareerItem;

namespace DevLearningCareerAPI.Services.Interfaces
{
    public interface ICareerItemService
    {
        Task CreateCareeritemAsync(CreateCareerItemDto careerItem);

        Task<CareerItemResponseDto> GetCareerItemByCareerIdAsync(Guid careerId);

        Task<CareerItemResponseDto> GetCareerItemByCourseIdAsync(Guid courseId);

        Task<CareerItemResponseDto> GetCareerItemByCareerCourseIdAsync(Guid careerId, Guid courseId);

        Task UpdateCareerItemAsync(Guid idCareer, Guid idCourse, UpdateCareerItemDto careerItemRequest);

        Task DeleteAllCareerItemByCareerIdAsync(Guid careerId);

        Task DeleteAllCareerItemByCourseIdAsync(Guid courseId);

        Task DeleteCareerItemByCareerCourseIdAsync(Guid careerId, Guid courseId);
    }
}
