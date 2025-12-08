using Dapper;
using DevLearningCourseCategoryAPI.Data;
using DevLearningCourseCategoryAPI.Repositories.Interfaces;
using Models.Models;
using Models.Models.Dtos.Author;
using Models.Models.Dtos.Course;

namespace DevLearningCourseCategoryAPI.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly DbConnectionFactory _connection;
    private readonly HttpClient _httpClienteAuthor;
    public CourseRepository(DbConnectionFactory connection, HttpClient httpClienteAuthor)
    {
        _connection = connection;
        _httpClienteAuthor = httpClienteAuthor;
    }

    public async Task<List<CourseResponseDto>> GetAllActivesCoursesAsync()
    {
        var sql = @"SELECT co.Id, co.Tag, co.Title, co.Summary, co.Url, co.DurationInMinutes, co.Level, 
                    co.CreateDate, co.LastUpdateDate, co.Active, co.Free, co.Featured, co.Tags, co.[AuthorId], ca.[Title] AS CategoryName 
					FROM Course co INNER JOIN Category ca ON co.CategoryId = ca.Id 
                    WHERE co.Active = 1
                    ORDER BY co.Title ASC";

        using (var con = _connection.GetConnection())
        {
            var courses = (await con.QueryAsync<CourseResponseDto>(sql)).ToList();
            var author = await _httpClienteAuthor.GetFromJsonAsync<List<AuthorResponseDto>>("");

            foreach (var course in courses)
            {
                var authorId = author?.FirstOrDefault(a => a.Id == course.AuthorId);
                if (authorId != null)
                {
                    course.AuthorName = authorId.Name;
                }
            }
            return courses;
        }
    }

    public async Task<CourseResponseDto?> GetCourseByIdAsync(Guid id)
    {
        var sql = @"SELECT co.Id, co.Tag, co.Title, co.Summary, co.Url, co.DurationInMinutes, co.Level, 
                    co.CreateDate, co.LastUpdateDate, co.Active, co.Free, co.Featured, co.Tags, co.[AuthorId], ca.[Title] AS CategoryName 
					FROM Course co
					JOIN Category ca
					ON co.CategoryId = ca.Id
					WHERE co.Id = @Id";

        using (var con = _connection.GetConnection())
        {
            var course = await con.QueryFirstOrDefaultAsync<CourseResponseDto>(sql, new { id });
            var author = await _httpClienteAuthor.GetFromJsonAsync<List<AuthorResponseDto>>("");

            var authorId = author?.FirstOrDefault(a => a.Id == course.AuthorId);
            if (authorId != null)
            {
                course.AuthorName = authorId.Name;
            }
            return course;
        }
    }

    public async Task<AuthorCategoryDto> GetAuthorCategoryId(Guid id)
    {
        var sql = @"SELECT AuthorId, CategoryId FROM Course WHERE Id = @Id";

        using (var con = _connection.GetConnection())
        {
            return await con.QueryFirstOrDefaultAsync<AuthorCategoryDto>(sql, new { id });
        }
    }

    public async Task CreateCourseAsync(Course course)
    {
        var sql = @"INSERT INTO Course ([Id],
										[Tag], 
										[Title], 
										[Summary], 
										[Url], 
										[Level], 
										[DurationInMinutes], 
										[CreateDate], 
										[LastUpdateDate], 
										[Active], 
										[Free],
										[Featured],
										[AuthorId], 
										[CategoryId], 
										[Tags])
				    VALUES (@Id,
							@Tag,
							@Title,
							@Summary,
							@Url,
							@Level,
							@DurationInMinutes,
							GETDATE(),
							GETDATE(),
							@Active,
							@Free,
							@Featured,
							@AuthorId,
							@CategoryId,
							@Tags)";

        using (var con = _connection.GetConnection())
        {
            await con.ExecuteAsync(sql, new
            {
                course.Id,
                course.Tag,
                course.Title,
                course.Summary,
                course.Url,
                course.Level,
                course.DurationInMinutes,
                course.Active,
                course.Free,
                course.Featured,
                course.AuthorId,
                course.CategoryId,
                course.Tags
            });
        }
    }

    public async Task UpdateCourseAsync(Guid id, Course course)
    {
        var sql = @"UPDATE Course SET [Tag] = @Tag, 
									  [Title] = @Title, 
									  [Summary] = @Summary, 
								      [Url] = @Url, 
								      [Level] = @Level, 
									  [DurationInMinutes] = @DurationInMinutes, 
									  [LastUpdateDate] = GETDATE(), 
									  [Active] = @Active, 
									  [Free] = @Free, 
									  [AuthorId] = @AuthorId, 
									  [CategoryId] = @CategoryId, 
									  [Tags] = @Tags 
					WHERE Id = @Id";

        using (var con = _connection.GetConnection())
        {
            await con.ExecuteAsync(sql, new
            {
                course.Tag,
                course.Title,
                course.Summary,
                course.Url,
                course.Level,
                course.DurationInMinutes,
                course.Active,
                course.Free,
                course.AuthorId,
                course.CategoryId,
                course.Tags,
                id
            });
        }
    }

    

    public async Task ActiveCourseAsync(Guid id)
    {
        var sql = @"UPDATE Course SET Active = 1
                    WHERE Id = @Id";

        using (var con = _connection.GetConnection())
        {
            await con.ExecuteAsync(sql, new { id });
        }
    }

    public async Task DeleteCourseAsync(Guid id)
    {
        var sql = @"UPDATE Course SET Active = 0
                    WHERE Id = @Id";

        using (var con = _connection.GetConnection())
        {
            await con.ExecuteAsync(sql, new { id });
        }
    }

    public async Task<List<CourseResponseDto>> GetAllCoursesOrderedAsync()
    {
        var sql = @"SELECT co.Id, co.Tag, co.Title, co.Summary, co.Url, co.DurationInMinutes, co.Level, 
					   co.CreateDate, co.LastUpdateDate, co.Active, co.Free, co.Featured, co.Tags, ca.[Title] AS CategoryName, co.[AuthorId]
				FROM Course co
				JOIN Category ca ON co.CategoryId = ca.Id
				ORDER BY co.Title ASC";


        using (var con = _connection.GetConnection())
        {
            var author = await _httpClienteAuthor.GetFromJsonAsync<List<AuthorResponseDto>>("");
            var courses = (await con.QueryAsync<CourseResponseDto>(sql)).ToList();

            foreach (var course in courses)
            {
                var authorId = author?.FirstOrDefault(a => a.Id == course.AuthorId);
                if (authorId != null)
                {
                    course.AuthorName = authorId.Name;
                }
            }
            return courses;
        }
    }
}