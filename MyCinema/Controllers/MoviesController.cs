using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCinema.Data;
using MyCinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCinema.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private CinemaDbContext _dbContext;

        public MoviesController(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Movies);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var movie = _dbContext.Movies.Find(id);

            if (movie == null)
            {
                return NotFound("No record found  against this Id");
            }

            return Ok(movie);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Movie movie)
        {
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movieObj)
        {
            var movie = _dbContext.Movies.Find(id);

            if (movie == null)
            {
                return NotFound("No record found  against this Id");
            }

            movie.Name = movieObj.Name;
            movie.Language = movieObj.Language;
            
            _dbContext.SaveChanges();
            return Ok("Record update successfully");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _dbContext.Movies.Find(id);

            if (movie == null)
            {
                return NotFound("No record found  against this Id");
            }

            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();

            return Ok("Record deleted");
        }
    }
}
