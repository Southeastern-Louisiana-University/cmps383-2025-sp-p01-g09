using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP25.Api.Data;
using Selu383.SP25.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TheaterController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<TheaterController> _logger;

        public TheaterController(DataContext context, ILogger<TheaterController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet(Name = "GetAllTheaters")]
        public async Task<ActionResult<IEnumerable<Theater>>> Get()
        {
            var theaters = await _context.Theaters.ToListAsync();
            if (theaters.Count == 0)
            {
                return NotFound("No theaters found.");
            }
            return Ok(theaters);
        }

        [HttpGet("{id}", Name = "GetTheaterById")]
        public async Task<ActionResult<Theater>> Get(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null)
            {
                _logger.LogWarning("Theater with ID {Id} not found.", id);
                return NotFound("Theater not found.");
            }

            return theater;
        }

        [HttpPost(Name = "CreateTheater")]
        public async Task<ActionResult<Theater>> Post([FromBody] Theater theater)
        {
            if (theater == null)
            {
                _logger.LogWarning("Invalid request: Theater data is missing.");
                return BadRequest("Theater data is required.");
            }

            if (string.IsNullOrEmpty(theater.Name) || string.IsNullOrEmpty(theater.Location))
            {
                _logger.LogWarning("Invalid request: Theater name or location is missing.");
                return BadRequest("Theater name and location are required.");
            }

            if (theater.Name.Length > 100)
            {
                _logger.LogWarning("Invalid request: Theater name exceeds 100 characters.");
                return BadRequest("Theater name is too long.");
            }

            _context.Theaters.Add(theater);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = theater.Id }, theater);
        }

        [HttpPut("{id}", Name = "UpdateTheater")]
        public async Task<IActionResult> Put(int id, [FromBody] Theater updatedTheater)
        {
            if (updatedTheater == null || id != updatedTheater.Id)
            {
                _logger.LogWarning("Invalid update request for theater ID {Id}.", id);
                return BadRequest();
            }

            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null)
            {
                return NotFound("Theater not found.");
            }

            theater.Name = updatedTheater.Name;
            theater.Location = updatedTheater.Location;
            theater.Notes = updatedTheater.Notes;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteTheater")]
        public async Task<IActionResult> Delete(int id)
        {
            var theater = await _context.Theaters.FindAsync(id);
            if (theater == null)
            {
                return NotFound("Theater not found.");
            }

            _context.Theaters.Remove(theater);
            await _context.SaveChangesAsync();

            if (await _context.Theaters.FindAsync(id) == null)
            {
                return NotFound("Theater was already deleted.");
            }

            return Ok($"Theater with ID {id} was deleted successfully."); 
        }
    }
}
