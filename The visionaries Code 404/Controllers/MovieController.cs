using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using The_visionaries_Code_404.Data;
using The_visionaries_Code_404.Services;
using X.PagedList;
using X.PagedList.Extensions;

namespace The_visionaries_Code_404.Controllers
{
    public class MovieController : Controller
    {
        private readonly ShopDbContext _db;
        private readonly IMovieService _movieService;
        public MovieController(ShopDbContext db, IMovieService movieService)
        {
            _db = db;
            _movieService = movieService;
        }

        [Route("Movies")]
        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            var result = _movieService.GetPagedMovies(page, pageSize);

            var totalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            ViewBag.PageSizeOptions = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5", Selected = pageSize == 5 },
                new SelectListItem { Text = "10", Value = "10", Selected = pageSize == 10 },
                new SelectListItem { Text = "20", Value = "20", Selected = pageSize == 20 }
            };

            return View(result.Movies);
        }

        public async Task<IActionResult> MovieDetails(int? id)
        {
            var movie = await _db.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieService.Add(movie);

                TempData["MovieMessage"] = $"{movie.Title} was added to the database.";
                TempData["AddedMovie"] = JsonConvert.SerializeObject(movie);
            }
            else
            {
                TempData["MovieMessage"] = "Something went wrong.";
            }

            return RedirectToAction("MovieAdded");
        }

        public IActionResult MovieAdded()
        {
            var movieData = TempData["AddedMovie"] as string;
            var addedMovie = !string.IsNullOrEmpty(movieData)
                ? JsonConvert.DeserializeObject<Movie>(movieData)
                : null;

            ViewBag.MovieMessage = TempData["MovieMessage"];

            return View(addedMovie);
        }


        public  IActionResult ListMovies()
        {
            return View( _db.Movies.ToList());
        }

        // Methods bellow methods are (as of now) "Secret methods" since they not yet follow structure criteria of project
        // NOT TO BE INCLUDED IN PRESENTATION IF NOT DONE
        public async Task<IActionResult> EditMovie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _db.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMovie(int id, [Bind("Id,Title,Director,ReleaseYear,Price,Image")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(movie);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        public async Task<IActionResult> DeleteMovie(int? id)
        {


            if (id == null)
            {
                return NotFound();
            }

            var movie = await _db.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost, ActionName("DeleteMovie")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _db.Movies.FindAsync(id);
            if (movie != null)
            {
                _db.Movies.Remove(movie);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _db.Movies.Any(e => e.Id == id);
        }

    }
}
