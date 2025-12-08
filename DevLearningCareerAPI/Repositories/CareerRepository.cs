using Dapper;
using DevLearningCareerAPI.Data;
using DevLearningCareerAPI.Repositories.Interfaces;
using DevLearningCareerAPI.Services;
using DevLearningCareerAPI.Services.Interfaces;
using Models.Models;
using Models.Models.Dtos.Career;
using Models.Models.Dtos.Course;

namespace DevLearningCareerAPI.Repositories;

public class CareerRepository : ICareerRepository
{

    #region Connection
    private readonly DbConnectionFactory _connection;
    private readonly ICareerItemService _service;

    public CareerRepository(DbConnectionFactory connection, ICareerItemService _service)
    {
        _connection = connection;
        this._service = _service;
    }

    #endregion

    public async Task CreateCareerAsync(Career career)
    {
        using (var con = _connection.GetConnection())
        {
            var sql = @"INSERT INTO Career(Id,Title,Summary,Url,DurationInMinutes,Active,Featured,Tags)
                                       VALUES (@CareerGuid,@CareerTitle,@CareerSumary,@CareerUrl,@CareerDurationInMinutes,@CareerActive,@CareerFeatured,@CareerTags);";

            await con.ExecuteAsync(sql, new
            {
                CareerGuid = career.Id,
                CareerTitle = career.Title,
                CareerSumary = career.Summary,
                CareerUrl = career.Url,
                CareerDurationInMinutes = career.DurationInMinutes,
                CareerActive = career.Active,
                CareerFeatured = career.Featured,
                CareerTags = career.Tags
            }
                                  );
        }

    }

    public async Task<List<CareerResponseDto>> GetAllCareersAsync()
    {
        using (var con = _connection.GetConnection())
        {

            var sql = @"SELECT CA.Id,CA.Title,CA.Summary,CA.Url,CA.DurationInMinutes,CA.Active,CA.Featured,CA.Tags,
                                   CI.CourseId AS CourseId,
                                   CI.Title,CI.Description, CI.[Order]
                            FROM Career CA
                            LEFT JOIN CareerItem CI ON CI.CareerId = CA.Id                      
                            ORDER BY CI.[Order];";

            var careerDictionary = new Dictionary<Guid, CareerResponseDto>();

            await con.QueryAsync<CareerResponseDto, ItemsResponseDto, CareerResponseDto>(sql,
                                                                                            (career, item) =>
                                                                                            {

                                                                                                if (!careerDictionary.TryGetValue(career.Id, out var existingCareer))

                                                                                                {

                                                                                                    careerDictionary.Add(career.Id, career);

                                                                                                    existingCareer = career;

                                                                                                }



                                                                                                if (item != null)

                                                                                                {

                                                                                                    existingCareer.Items.Add(item);


                                                                                                }
                                                                                                return existingCareer;
                                                                                            },
                                                                                            splitOn: "CourseId"
                                                                                        );

            var careers = careerDictionary.Values.ToList();


            foreach (var career in careers)
            {
                foreach (var item in career.Items)
                {
                    var course = await _service.GetCourseByIdAsync(item.CourseId);
                    item.TitleCourse = course.Title;
                }
            }

            return careers;
        }

    }
    public async Task<CareerResponseDto> GetCareerByIdAsync(Guid careerId)
    {
        using (var con = _connection.GetConnection())
        {
            var sql = @"SELECT CA.Id,CA.Title,CA.Summary,CA.Url,CA.DurationInMinutes,CA.Active,CA.Featured,CA.Tags,
                                   CI.CourseId AS CourseId,
                                   CI.Title,CI.Description, CI.[Order]
                            FROM Career CA
                            LEFT JOIN CareerItem CI ON CI.CareerId = CA.Id  
                            WHERE CA.Id = @CareerId
                            ORDER BY CI.[Order];";

            var careerDictionary = new Dictionary<Guid, CareerResponseDto>();

            await con.QueryAsync<CareerResponseDto, ItemsResponseDto, CareerResponseDto>(sql,
                                                                                            (career, item) =>
                                                                                            {

                                                                                                if (!careerDictionary.TryGetValue(career.Id, out var existingCareer))

                                                                                                {

                                                                                                    careerDictionary.Add(career.Id, career);

                                                                                                    existingCareer = career;

                                                                                                }

                                                                                                if (item != null)

                                                                                                {

                                                                                                    existingCareer.Items.Add(item);

                                                                                                }
                                                                                                return existingCareer;
                                                                                            },
                                                                                            param: new { CareerId = careerId },
                                                                                            splitOn: "CourseId"
                                                                                        );

            var career = careerDictionary.Values.FirstOrDefault();

            foreach (var careerItem in career.Items)
            {
                var course = await _service.GetCourseByIdAsync(careerItem.CourseId);
                careerItem.TitleCourse = course.Title;
            }
            return career;

        }

    }

    public async Task UpdateCareerAsync(Guid careerId, Career career)
    {
        using (var con = _connection.GetConnection())
        {
            var sql = @"UPDATE Career
                                     SET Title = @CareerTitle,
                                         Summary = @CareerSumary,
                                         Url = @CareerUrl,
                                         DurationInMinutes = @CareerDurationInMinutes,
                                         Active = @CareerActive,
                                         Featured = @CareerFeatured,
                                         Tags = @CareerTags
                                     WHERE Id = @CareerId;";

            await con.ExecuteAsync(sql, new
            {
                CareerId = careerId,
                CareerTitle = career.Title,
                CareerSumary = career.Summary,
                CareerUrl = career.Url,
                CareerDurationInMinutes = career.DurationInMinutes,
                CareerActive = career.Active,
                CareerFeatured = career.Featured,
                CareerTags = career.Tags
            });
        }
    }

    public async Task ChangeActiveAsync(Guid careerId)
    {
        using (var con = _connection.GetConnection())
        {
            var sql = @"UPDATE Career
                            SET Active = CASE Active WHEN 0 THEN 1
                                                     WHEN 1 THEN 0
                                                     END
                            WHERE Id = @CareerId;";

            await con.ExecuteAsync(sql, new { CareerId = careerId });
        }

    }

}
