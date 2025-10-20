using The_visionaries_Code_404.Data;
using The_visionaries_Code_404.Models;

namespace The_visionaries_Code_404.Services
{
    public class CartService : ICartService
    {
        private readonly ShopDbContext _db;

        public CartService(ShopDbContext db)
        {
            _db = db;
        }

        public CartItem GetCartItems(List<int> cartList)
        {
            CartItem cartItem = new CartItem
            {
                MovieList = new List<Movie>(),
                CartList = cartList,
                TotalPrice = 0m
            };

            foreach (var itemId in cartList)
            {
                var movie = _db.Movies.FirstOrDefault(m => m.Id == itemId);
                if (movie != null)
                {
                    if (!cartItem.MovieList.Contains(movie))
                    {
                        cartItem.MovieList.Add(movie);
                    }
                    cartItem.TotalPrice += movie.Price;
                }
            }

            return cartItem;
        }
    }
}
