using DevLearningAuthorAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Models.Dtos.Author;
using MongoDB.Bson;

namespace DevLearningAuthorAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorController : ControllerBase
	{
		private readonly AuthorService _service;

		public AuthorController(AuthorService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult<List<AuthorResponseDto>>> GetAllAuthorsAsync()
		{
			try
			{
				var authors = await _service.GetAllAuthorsAsync();

				if (authors.Count is 0)
					return NotFound("Register not found!");

				return Ok(authors);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

        [HttpGet("Active")]
        public async Task<ActionResult<List<AuthorResponseDto>>> GetAllActiveAuthorsAsync()
        {
            try
            {
                var authors = await _service.GetAllActiveAuthorsAsync();

                if (authors.Count is 0)
                    return NotFound("Register not found!");

                return Ok(authors);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{id}")]
		public async Task<ActionResult<AuthorResponseDto>> GetAuthorByIdAsync(ObjectId id)
		{
			try
			{
				var author = await _service.GetAuthorByIdAsync(id);

				if (author is null)
					return NotFound("Register not found!");

				return Ok(author);
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPost]
		public async Task<ActionResult> CreateAuthorAsync(CreateAuthorDto author)
		{
			try
			{
				await _service.CreateAuthorAsync(author);

				return Created();
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> UpdateAuthorAsync(ObjectId id, UpdateAuthorDto authorRequest)
		{
			try
			{
				if (await _service.GetAuthorByIdAsync(id) is null)
					return NotFound("Register not found!");

				await _service.UpdateAuthorAsync(id, authorRequest);

				return NoContent();
			}
			catch (Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> UpdateTypeAuthorAsync(ObjectId id)
		{
			try
			{
                if (_service.GetAuthorByIdAsync(id) is null)
                    return NotFound("Register not found!");

				await _service.UpdateTypeAuthorAsync(id);

				return NoContent();
			}
			catch(Exception ex)
			{
				return Problem(ex.Message);
			}
		}

		[HttpGet("/course/author/{id}")]
		public async Task<ActionResult> SelectAuthorCoursesAsync(string authorId)
		{
			throw new NotImplementedException();
			//if (!ObjectId.TryParse(authorId, out ObjectId objectId))
			//	return BadRequest("Id Inválido");

			//var result = await _service.SelectAuthorByCourseAsync(objectId);

			//if (result is null)
			//	return NotFound("Nenhum curso encontrado para esse autor");

			//return Ok(result);
		}
	}
}
