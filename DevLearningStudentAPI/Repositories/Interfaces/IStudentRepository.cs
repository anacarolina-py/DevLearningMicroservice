using Models.Models;
using Models.Models.Dtos.Student;

namespace DevLearningStudentAPI.Repositories.Interfaces;

public interface IStudentRepository
{
    public Task<List<StudentResponseDto>> GetAllStudentsAsync();
    public Task CreateStudentAsync(Student student);
    public Task UpdateEmailStudentAsync(Guid studentId, string email);
    public Task UpdatePhoneStudentAsync(Guid studentId, string phone);
    public Task<bool> DeleteStudentAsync(Guid studentId);



    Task<StudentResponseDto?> GetStudentByIdAsync(Guid studentId);
}
