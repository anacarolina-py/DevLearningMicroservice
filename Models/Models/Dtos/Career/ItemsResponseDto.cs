namespace Models.Models.Dtos.Career
{
    public class ItemsResponseDto
    {
        public Guid CourseId{ get; set; }

        public string TitleCourse { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public int Order { get; set; }
    }
    
}
