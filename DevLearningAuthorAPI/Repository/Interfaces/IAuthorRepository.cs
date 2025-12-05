using Models.Models;
using Models.Models.Dtos.Author;
using MongoDB.Bson;

namespace DevLearningAuthorAPI.Repository.Interfaces;

public interface IAuthorRepository
{
    Task<List<AuthorResponseDto>> GetAllActiveAuthorsAsync();
    Task<List<AuthorResponseDto>> GetAllAuthorsAsync();
    Task<AuthorResponseDto> GetAuthorByIdAsync(ObjectId id);
    Task CreateAuthorAsync(CreateAuthorDto author);
    Task UpdateAuthorAsync(ObjectId id, UpdateAuthorDto author);
    Task UpdateTypeAuthorAsync(ObjectId id);
}
