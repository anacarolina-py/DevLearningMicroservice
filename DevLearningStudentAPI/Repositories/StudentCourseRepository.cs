using Dapper;
using DevLearningStudentAPI.Data;
using DevLearningStudentAPI.Repositories.Interfaces;
using Models.Models;
using Models.Models.Dtos.Course;
using Models.Models.Dtos.StudantCourse;

namespace DevLearningStudentAPI.Repositories
{
    public class StudentCourseRepository
    {
        private readonly DbConnectionFactory _connection;
        private readonly HttpClient _httpClientCourse;
        public StudentCourseRepository(DbConnectionFactory connection, HttpClient httpClientCourse)
        {
            _connection = connection;
            _httpClientCourse = httpClientCourse;
        }

        public async Task<List<StudentCourseResponseDto>> GetAllStudentCoursesAsync()
        {
            var sql = @"SELECT
                        sc.Progress, 
                        sc.[Favorite], 
                        sc.StartDate, 
                        sc.LastUpdateDate AS RelationLastUpdateDate,
                        sc.StudentId as StudentId,
                        sc.CourseId as CourseId,
                        s.Birthdate,
                        s.Id as StudentId,
                        s.Name,
                        s.Email,
                        s.Document,
                        s.Phone,
                        s.CreateDate
                    FROM Student s
                    INNER JOIN StudentCourse sc
                    ON sc.StudentId = s.Id";

            using (var con = _connection.GetConnection())
            {
                var studentsCourse = await con.QueryAsync<StudentCourseDTO>(sql);
                var courses = await this._httpClientCourse.GetFromJsonAsync<List<CourseResponseDto>>("");
                var grouped = studentsCourse.GroupBy(sc => sc.StudentId);
                var result = new List<StudentCourseResponseDto>();
                foreach (var group in grouped)
                {
                    var first = group.First();

                    var courseWithRelation = group.Select(sc =>
                    {
                        var course = courses.FirstOrDefault(c => c.Id == sc.CourseId);

                        return new CourseWithRelationDto
                        {
                            Favorite = sc.Favorite,
                            Progress = sc.Progress,
                            LastUpdateDate = sc.RelationLastUpdateDate,
                            StartDate = sc.StartDate,
                            Course = course
                        };
                    }).ToList();
                    result.Add(new StudentCourseResponseDto
                    {
                        StudentId = first.StudentId,
                        Birthdate = first.Birthdate,
                        Courses = courseWithRelation,
                        CreateDate = first.CreateDate,
                        Document = first.Document,
                        Email = first.Email,
                        Name = first.Name,
                        Phone = first.Phone
                    });
                }

                return result;
            }
        }

        public async Task CreateStudentCourseAsync(Guid studentId, Guid courseId)
        {
            var sql = @"INSERT INTO StudentCourse(StudentId, CourseId, Progress, Favorite, StartDate) 
                    VALUES (@StudentId, @CourseId, @Progress, @Favorite, @StartDate)";


            using (var con = _connection.GetConnection())
            {
                var studentCourse = new StudentCourse(courseId, studentId);

                await con.ExecuteAsync(sql, new
                {
                    studentCourse.StudentId,
                    studentCourse.CourseId,
                    studentCourse.Progress,
                    studentCourse.Favorite,
                    StartDate = DateTime.Now
                });
            }
        }

        public async Task<int?> GetCourseDurationInMinutesAsync(Guid courseId)
        {
            var course = await this._httpClientCourse.GetFromJsonAsync<CourseResponseDto>(courseId.ToString());

            if (course is null)
                return null;

            var duration = course.DurationInMinutes;

            return duration;
        }

        public async Task<byte> GetProgressStudentCourseAsync(Guid studentId, Guid courseId)
        {
            var sql = @"SELECT Progress
                              FROM studentCourse
                              WHERE StudentId = @StudentId AND CourseId = @CourseId";

            using (var con = _connection.GetConnection())
            {
                return await con.QueryFirstOrDefaultAsync<byte>(sql, new { StudentId = studentId, CourseId = courseId });
            }
        }

        public async Task UpdateCourseProgressAsync(Guid studentId, Guid courseId, int minutesWatched, byte progress)
        {

            var sql = @"UPDATE StudentCourse 
                        SET Progress = @Progress, 
                            LastUpdateDate = GETDATE()
                        WHERE StudentId = @StudentId 
                        AND CourseId = @CourseId";

            using (var con = _connection.GetConnection())
            {
                await con.ExecuteAsync(sql, new { Progress = progress, StudentId = studentId, CourseId = courseId });
            }
        }


        public async Task<bool> UpdateFavoriteStudentCourse(Guid studentId, Guid courseId)
        {
            using (var con = _connection.GetConnection())
            {
                var sqlSelect = @"SELECT Favorite
                        FROM StudentCourse
                        WHERE StudentId = @StudentId AND CourseId = @CourseId";

                var sqlUpdate = @"UPDATE StudentCourse
                              SET Favorite = CASE Favorite WHEN 0 THEN 1
                                             WHEN 1 THEN 0
                                             END,
                              LastUpdateDate = GETDATE()
                              WHERE StudentId = @StudentId 
                              AND CourseId = @CourseId";

                await con.ExecuteAsync(sqlUpdate,
                    new { StudentId = studentId, CourseId = courseId });

                var favorite = await con.ExecuteScalarAsync<bool>(sqlSelect, new { StudentId = studentId, CourseId = courseId });

                return favorite;
            }
        }

    }

}
