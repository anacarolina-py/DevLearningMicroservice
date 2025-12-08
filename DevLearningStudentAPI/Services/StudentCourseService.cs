using DevLearningStudentAPI.Repositories;
using Models.Models;
using Models.Models.Dtos.Course;
using Models.Models.Dtos.StudantCourse;

namespace DevLearningStudentAPI.Services
{
    public class StudentCourseService
    {
        private readonly StudentCourseRepository _repository;

        public StudentCourseService(StudentCourseRepository repository)
        {
            _repository = repository;
        }

        public async Task<CourseStudentContadorDto> GetCountStudentCourseAsync(Guid courseId)
        {
            return await _repository.SelectCourseByStudentAsync(courseId);
        }

        public async Task<List<StudentCourseResponseDto>> GetAllStudentCoursesAsync()
        {
            return await _repository.GetAllStudentCoursesAsync();
        }

        public async Task CreateStudentCourseAsync(CreateStudentCourseDto studentCourse)
        {
            var newStudentCourse =  new StudentCourse
            (
                studentCourse.StudentId,
                studentCourse.CourseId
            );
            if (studentCourse.StudentId == Guid.Empty || studentCourse.CourseId == Guid.Empty)
            {
                throw new ArgumentException("StudentId and CourseId must be valid GUID's.");
            }

            await _repository.CreateStudentCourseAsync(studentCourse.StudentId, studentCourse.CourseId);
        }

        public async Task UpdateCourseProgressAsync(Guid studentId, Guid courseId, int minutesWatched)
        {
            var duration = await _repository.GetCourseDurationInMinutesAsync(courseId);
            if (duration == null)
                return;


            double percent = (minutesWatched / (double)duration) * 100;

            if (percent > 100)
                percent = 100;

            byte progress = (byte)percent;
            await _repository.UpdateCourseProgressAsync(studentId, courseId, minutesWatched, progress);
        }


        public async Task<bool> UpdateFavoriteStudentCourse(CreateStudentCourseDto studentCourse)
        {
            return await _repository.UpdateFavoriteStudentCourse(studentCourse.StudentId, studentCourse.CourseId);
        }

        internal async Task<bool> GetRelationStudentCourseAsync(Guid studentId, Guid courseId)
        {

            var existsRelationShip = await _repository.GetProgressStudentCourseAsync(studentId, courseId);
            if (existsRelationShip == null)
                return false;

            return true;
        }
    }
}
