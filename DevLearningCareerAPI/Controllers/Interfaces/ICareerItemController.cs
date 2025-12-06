using Microsoft.AspNetCore.Mvc;
using Models.Models.Dtos.CareerItem;

namespace DevLearningCareerAPI.Controllers.Interfaces
{
    public interface ICareerItemController
    {
        Task<ActionResult> CreateCareeritemAsync(CreateCareerItemDto careerItem);

        Task<ActionResult> UpdateCareerItemAsync(UpdateCareerItemDto careerItem);

        Task<ActionResult> DeleteAllCareerItemByCareerIdAsync(Guid careerId);

        Task<ActionResult> DeleteAllCareerItemByCourseIdAsync(Guid courseId);

        Task<ActionResult> DeleteCareerItemByCareerCourseIdAsync(Guid careerId, Guid courseId);

    }
}
