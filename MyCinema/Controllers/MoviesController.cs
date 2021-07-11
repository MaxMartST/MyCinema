using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCinema.Data;
using MyCinema.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
                return NotFound("No record found against this Id");
            }

            return Ok(movie);
        }

        [Authorize]
        [HttpGet("[action]")]
        public IActionResult AllMovies()
        {
            var movies = from movie in _dbContext.Movies
                         select new
                         { 
                            Id = movie.Id,
                            Name = movie.Name,
                            Duration = movie.Duration,
                            Language = movie.Language,
                            Rating = movie.Rating,
                            Genre = movie.Genre,

                         };

            return Ok(movies);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Post([FromForm] Movie movieObj)
        {
            var guid = Guid.NewGuid();
            var filePath = Path.Combine("wwwroot", guid + ".jpg");

            if (movieObj.Image != null) 
            {
                var fileStream = new FileStream(filePath, FileMode.Create);
                movieObj.Image.CopyTo(fileStream);
            }

            movieObj.ImageUrl = filePath.Remove(0, 7);

            _dbContext.Movies.Add(movieObj);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromForm] Movie movieObj)
        {
            var movie = _dbContext.Movies.Find(id);

            if (movie == null)
            {
                return NotFound("No record found against this Id");
            }
            else 
            {
                var guid = Guid.NewGuid();
                var filePath = Path.Combine("wwwroot", guid + ".jpg");

                if (movieObj.Image != null)
                {
                    var fileStream = new FileStream(filePath, FileMode.Create);
                    movieObj.Image.CopyTo(fileStream);
                    movieObj.ImageUrl = filePath.Remove(0, 7);
                }

                movie.Name = movieObj.Name;
                movie.Description = movieObj.Description;
                movie.Language = movieObj.Language;
                movie.Duration = movieObj.Duration;
                movie.PlayingDate = movieObj.PlayingDate;
                movie.PlayingTime = movieObj.PlayingTime;
                movie.Rating = movieObj.Rating;
                movie.Genre = movieObj.Genre;
                movie.TrageUrl = movieObj.TrageUrl;
                movie.TicketPrice = movieObj.TicketPrice;

                _dbContext.SaveChanges();

                return Ok("Record updated successfully");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _dbContext.Movies.Find(id);

            if (movie == null)
            {
                return NotFound("No record found against this Id");
            }
            else 
            {
                _dbContext.Movies.Remove(movie);
                _dbContext.SaveChanges();

                return Ok("Record deteled");
            }
        }
    }
}
