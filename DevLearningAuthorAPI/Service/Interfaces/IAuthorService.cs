using Models.Models.Dtos.Author;
using MongoDB.Bson;

namespace DevLearningAuthorAPI.Service.Interfaces;

public interface IAuthorService
{
    Task<List<AuthorResponseDto>> GetAllActiveAuthorsAsync();
    Task<List<AuthorResponseDto>> GetAllAuthorsAsync();
    Task<AuthorResponseDto> GetAuthorByIdAsync(ObjectId id);
    Task CreateAuthorAsync(CreateAuthorDto author);
    Task UpdateAuthorAsync(ObjectId id, UpdateAuthorDto authorRequest);
    Task UpdateTypeAuthorAsync(ObjectId id);
}
