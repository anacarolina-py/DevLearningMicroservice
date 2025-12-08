using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Dtos.StudantCourse
{
    public class StudentCourseDTO
    {
        public byte Progress { get; set; }
        public bool Favorite { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime RelationLastUpdateDate { get; set; }
        public Guid StudentId { get; set; }
        public Guid CourseId { get; set; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Document { get; init; } = string.Empty;
        public string Phone { get; init; } = string.Empty;
        public DateTime Birthdate { get; init; }
        public DateTime CreateDate { get; init; }
    }
}
