using The_visionaries_Code_404.Data;

namespace The_visionaries_Code_404.Models
{
    public class OrderSummary
    {
        public Customer Customer { get; set; }
        public List<Movie> PurchasedMovies { get; set; }
        public List<int> CartList { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
