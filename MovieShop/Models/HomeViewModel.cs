using The_visionaries_Code_404.Data;

namespace The_visionaries_Code_404.Models
{
    public class HomeViewModel
    {
        public List<Movie> MostPopularMovies { get; set; } = [];
        public List<Movie> NewestFiveMovies { get; set; } = [];
        public List<Movie> OldestFiveMovies { get; set; } = [];
        public List<Movie> CheapestFiveMovies { get; set; } = [];
        public Customer? CustomerMostExpensiveOrder { get; set; }
    }
}
