using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using The_visionaries_Code_404.Data;

namespace The_visionaries_Code_404.Services
{
    public class MovieService : IMovieService
    {
        private readonly ShopDbContext _shopDbContext;

        public MovieService(ShopDbContext shopDbContext)
        {
            _shopDbContext = shopDbContext;
        }
        public (List<Movie> Movies, int TotalCount) GetPagedMovies(int pageNumber, int pageSize)
        {
            var query = _shopDbContext.Movies.AsQueryable();

            var totalCount = query.Count();


            var movies = query
                .OrderBy(m => m.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return (movies, totalCount);
        }
       
        public void Add(Movie movie)
        {
            _shopDbContext.Movies.Add(movie);
            _shopDbContext.SaveChanges();
        }

        public List<Movie> GetMostPopularMovies()
        {
            if (_shopDbContext.OrderRows.Any() )
            {
                var movies = _shopDbContext.OrderRows.GroupBy(oR => oR.MovieId)
                    .Select(
                    g => new
                    {
                        Id = g.Key,
                        MovieCount = g.Count()
                    }).OrderByDescending(o => o.MovieCount)
                    .Join(_shopDbContext.Movies, g => g.Id, m => m.Id,
                    (g, m) => new Movie
                    {
                        Id = m.Id,
                        Title = m.Title,
                        Director = m.Director,
                        ReleaseYear = m.ReleaseYear,
                        Price = m.Price,
                        Image = m.Image
                    }).ToList();

                return movies;
            }
            else
                return new List<Movie>();
        }

        public List<Movie> GetFiveNewestMovies()
        {
            if (_shopDbContext.Movies.Any())
            {
                var movies = _shopDbContext.Movies.OrderByDescending(m => m.ReleaseYear).Take(5).ToList();
                return movies;
            }
            else
                return new List<Movie>();
        }

        public List<Movie> GetFiveOldestMovies()
        {
            if (_shopDbContext.Movies.Any())
            {
                var movies = _shopDbContext.Movies.OrderBy(m => m.ReleaseYear).Take(5).ToList();
                return movies;
            }
            else
                return new List<Movie>();
        }

        public List<Movie> GetFiveCheapestMovies()
        {
            if (_shopDbContext.Movies.Any())
            {
                var movies = _shopDbContext.Movies.OrderBy(m => m.Price).Take(5).ToList();
                return movies;
            }
            else
                return new List<Movie>();
        }
    }
}
