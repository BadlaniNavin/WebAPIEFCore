using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIEFCoreDemo.Configuration;
using WebAPIEFCoreDemo.Contexts;
using WebAPIEFCoreDemo.Models;

namespace WebAPIEFCoreDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MovieContext _dbContext;
        private ConnectionStringSettings _connectionStringSettings;
        private ILogger<MoviesController> _logger;
        public MoviesController(MovieContext dbContext, ConnectionStringSettings connectionStringSettings, ILogger<MoviesController> logger)
        {
            _dbContext = dbContext;
            _connectionStringSettings = connectionStringSettings;
            _logger = logger;

        }
        //GET: api/movies
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            _logger.LogDebug("WeatherController GetMoviews EP Called");
            if(_dbContext.Movies == null)
            {
                return NotFound();
            }
            return Ok(await _dbContext.Movies.ToListAsync());
        }

        //GET: api/movies/1
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovies(int id)
        {
            if (await _dbContext.Movies.FindAsync(id) == null)
            {
                return NotFound();
            }
            return Ok(await _dbContext.Movies.FindAsync(id));
        }
        //POST: api/movies
        [HttpPost]
        public async Task<IActionResult> Save(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            return Ok(movie);
        }
        //PUT: api/movies
        [HttpPut]
        public async Task<IActionResult> Update(Movie movie)
        {
            Movie objMovie = await _dbContext.Movies.FindAsync(movie.Id);
            if (objMovie == null)
            {
                return NotFound();
            }
            _dbContext.Entry(objMovie).State = EntityState.Detached;
            _dbContext.Update(movie);
            _dbContext.SaveChanges();
            return Ok(movie);
        }
        //DELETE: api/movies/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Movie objMovie = await _dbContext.Movies.FindAsync(id);
            if (objMovie == null)
            {
                return NotFound();
            }
            _dbContext.Movies.Remove(objMovie);
            _dbContext.SaveChanges();
            return Ok($"Movie Deleted with id {id}");
        }

    }
}
