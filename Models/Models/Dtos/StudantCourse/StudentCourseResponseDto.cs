<<<<<<< HEAD
﻿using Models.Models.Dtos.Course;
using Models.Models.Dtos.Student;

namespace Models.Models.Dtos.StudantCourse
=======
﻿namespace Models.Models.Dtos.StudantCourse
>>>>>>> c6d20d2eab84d0303edf74bce267bbba4c484478
{
    public class StudentCourseResponseDto
    {
        public Guid StuId { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Document { get; init; } = string.Empty;
        public string Phone { get; init; } = string.Empty;
        public DateTime Birthdate { get; init; }
        public DateTime CreateDate { get; init; }
        public List<CourseWithRelationDto> Courses { get; init; } = [];
    }
}
