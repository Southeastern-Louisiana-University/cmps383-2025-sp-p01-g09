using Microsoft.AspNetCore.Mvc;
using Selu383.SP25.Api.Data;
using System.Diagnostics.Eventing.Reader;
using Selu383.SP25.Api.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Selu383.SP25.Api.Controllers
{
    [ApiController]
    [Route("/api/theaters")]
    public class TheaterController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public TheaterController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _dataContext
                .Set<Theater>()
                .Select(Theater => new TheaterGetDto
                {
                    Id = Theater.Id,
                    Name = Theater.Name,
                    Address = Theater.Address,
                    SeatCount = Theater.SeatCount
                }).ToList();

            return Ok(data); 
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var data = _dataContext
                .Set<Theater>()
                .Where(Theater => Theater.Id == id)
                .Select(Theater => new TheaterGetDto
                {
                    Id = Theater.Id,
                    Name = Theater.Name,
                    Address = Theater.Address,
                    SeatCount = Theater.SeatCount
                }).FirstOrDefault();

            if (data == null)
            {
                return NotFound();  
            }

            return Ok(data);  
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            
            var theater = _dataContext.Set<Theater>().FirstOrDefault(t => t.Id == id);

            
            if (theater == null)
            {
                return NotFound(); 
            }

            
            _dataContext.Set<Theater>().Remove(theater);
            _dataContext.SaveChanges(); 

            
            return Ok(); 
        }

        [HttpPost]
        public IActionResult CreateTheater([FromBody] TheaterCreateDto createDto)
        {
            
            if (string.IsNullOrEmpty(createDto.Name))
            {
                return BadRequest("Name cannot be empty");
            }
            if (createDto.Name.Length > 120)
            {
                return BadRequest("Name is too long");
            }
            if (string.IsNullOrEmpty(createDto.Address))
            {
                return BadRequest("Address cannot be empty");
            }
            if (createDto.SeatCount < 1)
            {
                return BadRequest("Theater must have at least one seat");
            }

            
            var theaterToCreate = new Theater
            {
                Name = createDto.Name,
                Address = createDto.Address,
                SeatCount = createDto.SeatCount
            };

            
            _dataContext.Set<Theater>().Add(theaterToCreate);
            _dataContext.SaveChanges();

            
            var theaterReturn = new TheaterGetDto
            {
                Id = theaterToCreate.Id,
                Name = theaterToCreate.Name,
                Address = theaterToCreate.Address,
                SeatCount = theaterToCreate.SeatCount
            };

            
            return CreatedAtAction(nameof(GetById), new { id = theaterToCreate.Id }, theaterReturn);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTheater([FromBody] TheaterUpdateDto updateDto, int id)
        {
            var TheaterToUpdate = _dataContext.Set<Theater>()
                .FirstOrDefault(Theater => Theater.Id == id);

            if (string.IsNullOrEmpty(updateDto.Name))
            {
                return BadRequest("Name cannot be empty");
            }
            if (updateDto.Name.Length > 120)
            {
                return BadRequest("Name is too long");
            }
            if (string.IsNullOrEmpty(updateDto.Address))
            {
                return BadRequest("Address cannot be empty");
            }

            TheaterToUpdate.Name = updateDto.Name;
            TheaterToUpdate.Address = updateDto.Address;
            TheaterToUpdate.SeatCount = updateDto.SeatCount;

            _dataContext.SaveChanges();

            var TheaterReturn = new TheaterGetDto
            {
                Id = TheaterToUpdate.Id,
                Name = TheaterToUpdate.Name,
                Address = TheaterToUpdate.Address,
                SeatCount = TheaterToUpdate.SeatCount
            };

            return Ok(TheaterReturn);

        }


    }
}
