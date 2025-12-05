using DevLearningAPI.Models;
using DevLearningAPI.Models.Dtos.Career;

namespace DevLearningCareerAPI.Repositories.Interfaces
{
    public interface ICareerRepository
    {
        Task CreateCareerAsync(Career career);

        Task<List<CareerResponseDto>> GetAllCareersAsync();

        Task<CareerResponseDto> GetCareerByIdAsync(Guid careerId);

        Task UpdateCareerAsync(Guid careerId,Career career);

        Task ChangeActiveAsync(Guid careerId);
    }
}
