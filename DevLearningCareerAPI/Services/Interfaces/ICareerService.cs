using Microsoft.AspNetCore.Mvc;
using Models.Models.Dtos.Career;

namespace DevLearningCareerAPI.Services.Interfaces
{
    public interface ICareerService
    {
        Task CreateCareerAsync(CreateCareerDto career);

        Task<List<CareerResponseDto>> GetAllCareersAsync();

        Task<CareerResponseDto> GetCareerByIdAsync(Guid careerId);

        Task UpdateCareerAsync(Guid careerId, UpdateCareerDto careerRequest);

        Task ChangeActiveAsync(Guid careerId);
    }
}
