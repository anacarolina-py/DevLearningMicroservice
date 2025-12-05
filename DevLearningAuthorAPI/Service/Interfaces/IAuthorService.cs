using DevLearningAPI.Models.Dtos.Author;

namespace DevLearningAuthorAPI.Service.Interfaces;

public interface IAuthorService
{
    Task<List<AuthorResponseDto>> GetAllActiveAuthorsAsync();
    Task<List<AuthorResponseDto>> GetAllAuthorsAsync();
    Task<AuthorResponseDto> GetAuthorByIdAsync(Guid id);
    Task CreateAuthorAsync(CreateAuthorDto author);
    Task UpdateAuthorAsync(Guid id, UpdateAuthorDto authorRequest);
    Task UpdateTypeAuthorAsync(Guid id);
}
