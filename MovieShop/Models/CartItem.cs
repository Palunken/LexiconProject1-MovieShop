using The_visionaries_Code_404.Data;

namespace The_visionaries_Code_404.Models
{
    public class CartItem
    {
        public List<Movie> MovieList { get; set; } = new List<Movie>();
        public List<int> CartList { get; set; } = new List<int>();
        public decimal TotalPrice { get; set; } = 0m;
    }
}
