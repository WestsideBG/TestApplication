using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using MoviesApplication.Models;

namespace MoviesApplication.Data
{
    public class ApplicationDBContext : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        public Movie GetMovieById(int id)
        {
            var movie = Movies.FirstOrDefault(m => m.Id == id);

            if (movie != null)
            {
                return movie;
            } else
            {
                throw new InvalidOperationException($"Movie with ID: {id} not found");
            }
        }

        public Movie GetMovieByTitle(string title)
        {
            var movie = Movies.FirstOrDefault(m => m.Title == title);

            if (movie != null)
            {
                return movie;
            }
            else
            {
                throw new InvalidOperationException($"Movie with Title: {title} not found");

            }
        }

        public List<Movie> GetMoviesByTitle(string title)
        {
            List<Movie> movies = Movies.Where(m => m.Title.Contains(title)).ToList();

            if (movies.Count == 0)
            {
                throw new InvalidOperationException($"Movies with title {title} were not found!");
            }

            return movies;
        }

        public List<Movie> GetAllMovies()
        {
            return Movies.ToList();
        }
    }
}
