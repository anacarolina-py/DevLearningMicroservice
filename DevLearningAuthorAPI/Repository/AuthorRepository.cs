using DevLearningAuthorAPI.Data;
using DevLearningAuthorAPI.Repository.Interfaces;
using Models.Models;
using Models.Models.Dtos.Author;
using Models.Models.Dtos.Course;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DevLearningAuthorAPI.Repository { 

public class AuthorRepository : IAuthorRepository
{
	private readonly IMongoCollection<Author> _authorsCollection;
	private readonly DbConnectionFactory _connection;
	private readonly ILogger<AuthorRepository> _logger;
	private readonly HttpClient _httpClient;
	
	public AuthorRepository(DbConnectionFactory connection, 
		ILogger<AuthorRepository> logger,
		HttpClient httpClient)
	{
		_connection = connection;
		_authorsCollection = _connection.GetMongoCollection();
		_logger = logger;
		_httpClient = httpClient;
		_httpClient.BaseAddress = new Uri("https://localhost7242/api/course/author/id");
	}

	public async Task<List<AuthorResponseDto>> GetAllActiveAuthorsAsync()
	{

		var authors = await _authorsCollection.Find(author => author.Type == 1).ToListAsync();

		return authors.Select(author => new AuthorResponseDto
		{
			Id = author.Id.ToString(),
			Name = author.Name,
			Title = author.Title,
			Image = author.Image,
			Bio = author.Bio,
			Url = author.Url,
			Email = author.Email,
			Type = author.Type

		}).ToList();

	}

	public async Task<List<AuthorResponseDto>> GetAllAuthorsAsync()
	{
		var authors = await _authorsCollection.Find(_ => true).ToListAsync();

		return authors.Select(author => new AuthorResponseDto
		{
			Id = author.Id.ToString(),
			Name = author.Name,
			Title = author.Title,
			Image = author.Image,
			Bio = author.Bio,
			Url = author.Url,
			Email = author.Email,
			Type = author.Type

		}).ToList();
	}

	public async Task<AuthorResponseDto> GetAuthorByIdAsync(ObjectId id)
	{

		var author = await _authorsCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

		if (author == null)
			return null;

		return new AuthorResponseDto
		{
			Id = author.Id.ToString(),
			Name = author.Name,
			Title = author.Title,
			Image = author.Image,
			Bio = author.Bio,
			Url = author.Url,
			Email = author.Email,
			Type = author.Type

		};
	}

	public async Task CreateAuthorAsync(CreateAuthorDto author)
	{
		try
		{
			var newAuthor = new Author(

				author.Name,
				author.Title,
				author.Image,
				author.Bio,
				author.Url,
				author.Email

				);

			await _authorsCollection.InsertOneAsync(newAuthor);
		}
		catch (Exception ex)
		{
			_logger.LogError($"Error creating author");

		}
	}

	public async Task UpdateAuthorAsync(ObjectId id, UpdateAuthorDto author)
	{
            var updates = new List<UpdateDefinition<Author>>();
            var builder = Builders<Author>.Update;

            if (!string.IsNullOrWhiteSpace(author.Name))
                updates.Add(builder.Set(a => a.Name, author.Name));

            if (!string.IsNullOrWhiteSpace(author.Title))
                updates.Add(builder.Set(a => a.Title, author.Title));

            if (!string.IsNullOrWhiteSpace(author.Image))
                updates.Add(builder.Set(a => a.Image, author.Image));

            if (!string.IsNullOrWhiteSpace(author.Bio))
                updates.Add(builder.Set(a => a.Bio, author.Bio));

            if (!string.IsNullOrWhiteSpace(author.Url))
                updates.Add(builder.Set(a => a.Url, author.Url));

            if (!string.IsNullOrWhiteSpace(author.Email))
                updates.Add(builder.Set(a => a.Email, author.Email));


            if (author.Type.HasValue)
                updates.Add(builder.Set(a => a.Type, author.Type.Value));

            if (!updates.Any())
                return;

            await _authorsCollection.UpdateOneAsync(
                a => a.Id == id,
                builder.Combine(updates)
            );
        }

	public async Task UpdateTypeAuthorAsync(ObjectId id)
	{
		var author = await _authorsCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

		if (author == null) return;

		var newType = author.Type == 0 ? 1 : 0;

		var update = Builders<Author>.Update.Set(a => a.Type, newType);

		await _authorsCollection.UpdateOneAsync(a => a.Id == id, update);
	}

	public async Task<ContadorAuthorDto> SelectAuthorByCourseAsync(ObjectId authorId)
	{
		Guid authorGuid;

		if(!Guid.TryParse(authorId.ToString(), out authorGuid))
			return null;

		var result = await _httpClient.GetFromJsonAsync<ContadorAuthorDto>($"author/{authorGuid}");

		return result;
		}
	}
}