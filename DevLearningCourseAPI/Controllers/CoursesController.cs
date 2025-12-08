using DevLearningCourseCategoryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Dtos.Course;


namespace DevLearningCourseCategoryAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoursesController : ControllerBase
	{
		private readonly ICourseService _service;
		private readonly HttpClient _httpClientStudentCourse;

		public CoursesController(ICourseService service, IHttpClientFactory httpClientStudentCourse)
		{
			_service = service;
			_httpClientStudentCourse = httpClientStudentCourse.CreateClient();
		}

        [HttpGet]
        public async Task<ActionResult<List<CourseResponseDto>>> GetAllCoursesOrdered()
        {
            try
            {
                var courses = await _service.GetAllCoursesOrderedAsync();

                if (courses.Count is 0)
                    return NotFound("Register not found!");

                return Ok(courses);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

		[HttpGet("all")]
		public async Task<ActionResult<List<CourseResponseDto>>> GetAllCourses()
		{
			try
			{
				var courses = await _service.GetAllCoursesAsync();

				if (courses.Count is 0)
					return NotFound("Register not found!");

				return Ok(courses);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}


        [HttpGet("{id}")]
		public async Task<ActionResult<CourseResponseDto>> GetCourseById(Guid id)
		{
			try
			{
				var course = await _service.GetCourseByIdAsync(id);

				if (course is null)
					return NotFound("Register not found!");

				return Ok(course);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult> CreateCourse(CreateCourseDto course)
		{
			try
			{
				await _service.CreateCourseAsync(course);
				return Created();
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateCourse(Guid id, UpdateCourseDto course)
		{
			try
			{
				if (await _service.GetCourseByIdAsync(id) is null)
					return NotFound("Register not found!");

				await _service.UpdateCourseAsync(id, course);
				return Ok();
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPut("active/{id}")]
		public async Task<ActionResult> ActiveCourse(Guid id)
		{
			try
			{
                if (await _service.GetCourseByIdAsync(id) is null)
                    return NotFound("Register not found!");

                await _service.ActiveCourseAsync(id);
				return NoContent();
            }
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> DeleteCourse(Guid id)
		{
			try
			{
				if (await _service.GetCourseByIdAsync(id) is null)
					return NotFound("Register not found!");

				var existCourse = await _httpClientStudentCourse.GetFromJsonAsync<CourseStudentContadorDto>($"https://localhost:7115/api/StudentCourse/{id.ToString()}");
                if (existCourse.Quantidade > 0)
                {
                    return BadRequest("Não foi possível inutilizar o curso: vínculo entre aluno e curso já existe.");
                }

                await _service.DeleteCourseAsync(id);
				return NoContent();
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}
	}
}
