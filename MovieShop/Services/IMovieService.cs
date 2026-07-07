using The_visionaries_Code_404.Data;

namespace The_visionaries_Code_404.Services
{
    public interface IMovieService
    {
        public (List<Movie> Movies, int TotalCount) GetPagedMovies(int pageNumber, int pageSize);
        public void Add(Movie movie);
        public List<Movie> GetMostPopularMovies();
        public List<Movie> GetFiveNewestMovies();
        public List<Movie> GetFiveOldestMovies();
        public List<Movie> GetFiveCheapestMovies();


    }
}
