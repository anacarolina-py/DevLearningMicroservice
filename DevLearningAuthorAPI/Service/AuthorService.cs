using DevLearningAuthorAPI.Repository;
using DevLearningAuthorAPI.Service.Interfaces;
using Models.Models;
using Models.Models.Dtos.Author;
using Models.Models.Dtos.Course;
using MongoDB.Bson;

namespace DevLearningAuthorAPI.Service;

public class AuthorService : IAuthorService
{
    private readonly AuthorRepository _repository;
    private readonly HttpClient _httpClient;

    public AuthorService(AuthorRepository repository, HttpClient httpClient)
    {
        _repository = repository;
        _httpClient = httpClient;
    }

    public async Task<List<AuthorResponseDto>> GetAllActiveAuthorsAsync()
    {
        return await _repository.GetAllActiveAuthorsAsync();
    }

    public async Task<List<AuthorResponseDto>> GetAllAuthorsAsync()
    {
        var authors = await _repository.GetAllAuthorsAsync();
        return authors.OrderBy(x => x.Name).ToList();
    }

    public async Task<AuthorResponseDto> GetAuthorByIdAsync(ObjectId id)
    {
        return await _repository.GetAuthorByIdAsync(id);
    }

    public async Task CreateAuthorAsync(CreateAuthorDto author)
    {
        try
        {
            await _repository.CreateAuthorAsync(author);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating author {ex.Message}");
        }
    }

    public async Task UpdateAuthorAsync(ObjectId id, UpdateAuthorDto authorRequest)
    {
       
        await _repository.UpdateAuthorAsync(id, authorRequest);
    }

    public async Task UpdateTypeAuthorAsync(ObjectId id)
    {
        await _repository.UpdateTypeAuthorAsync(id);
    }

    public async Task<ContadorAuthorDto> SelectAuthorByCourseAsync(ObjectId authorId)
    {
        return await _repository.SelectAuthorByCourseAsync(authorId);
    } 
}
