using MongoDB.Bson.Serialization.Attributes;

namespace Models.Models.Dtos.Course
{
    public class ContadorAuthorDto
    {
        public int Quantidade { get; init; }
        public List<CourseResponseDto> Cursos { get; set; }
    }
}
