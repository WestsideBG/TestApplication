using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApplication.Data;
using MoviesApplication.Models;

namespace MoviesApplication.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public MoviesController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: MoviesController
        public ActionResult Index(string searchTitle)
        {
            List<Movie> movies = new List<Movie>();
            if (searchTitle != null)
            {
                movies = _dbContext.GetMoviesByTitle(searchTitle).ToList();
            }
            else
            {
                movies = _dbContext.GetAllMovies();
            }
            
            return View(movies);
        }

        // GET: MoviesController/Details/5
        public ActionResult Details(int id)
        {
            var movie = _dbContext.GetMovieById(id);

            return View(movie);
        }

        // GET: MoviesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string title, string producer, DateTime releaseDate, string description, string genre)
        {
            Movie movie = new Movie(title, description, genre, producer, releaseDate);
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: MoviesController/Edit/5
        public ActionResult Edit(int id)
        {
            var movie = _dbContext.GetMovieById(id);

            return View(movie);
        }

        // POST: MoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, string title, string description, DateTime releaseDate, string producer, string genre)
        {
            var movie = _dbContext.GetMovieById(id);

            movie.UpdateTitle(title);
            movie.UpdateDescription(description);
            movie.UpdateReleaseDate(releaseDate);
            movie.UpdateProducer(producer);
            movie.UpdateGenre(genre);

            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: MoviesController/Delete/5
        public ActionResult Delete(int id)
        {
            var movie = _dbContext.GetMovieById(id);
            return View(movie);
        }

        // POST: MoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var movie = _dbContext.GetMovieById(id);
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
