using DevLearningAuthorAPI.Data;
using DevLearningAuthorAPI.Repository.Interfaces;
using Models.Models;
using Models.Models.Dtos.Author;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DevLearningAuthorAPI.Repository;

public class AuthorRepository : IAuthorRepository
{
	private readonly IMongoCollection<Author> _authorsCollection;
	private readonly DbConnectionFactory _connection;
	private readonly ILogger<AuthorRepository> _logger;
	
	public AuthorRepository(DbConnectionFactory connection, ILogger<AuthorRepository> logger)
	{
		_connection = connection;
		_authorsCollection = _connection.GetMongoCollection();
		_logger = logger;
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
		var updateAuthor = new Author(
			author.Name,
			author.Title,
			author.Image,
			author.Bio,
			author.Url,
			author.Email
			);

		var updateResult = await _authorsCollection.ReplaceOneAsync
			(a => a.Id == id, updateAuthor);

		if (updateResult.MatchedCount == 0)
		{
			_logger.LogWarning($"No author with this id found to update");
		}
	}

	public async Task UpdateTypeAuthorAsync(ObjectId id)
	{
		var author = await _authorsCollection.Find(a => a.Id == id).FirstOrDefaultAsync();

		if (author == null) return;

		var newType = author.Type == 0 ? 1 : 0;

		var update = Builders<Author>.Update.Set(a => a.Type, newType);

		await _authorsCollection.UpdateOneAsync(a => a.Id == id, update);
	}

	public async Task<ContadorAuthorDto?> SelectAuthorByCourseAsync(ObjectId authorId)
	{
		var sql = @"SELECT COUNT(Id) AS Quantidade FROM Course WHERE AuthorId = @AuthorId";

		Guid autorGuid;
		Guid.TryParse(authorId.ToString(), out autorGuid);

		using (var con = _connection.GetConnection())
		{
			return await con.QueryFirstOrDefaultAsync<ContadorAuthorDto>(sql, new { authorId });


			throw new NotImplementedException();

		}
	}
}