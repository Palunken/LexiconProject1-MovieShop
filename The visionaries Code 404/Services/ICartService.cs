using The_visionaries_Code_404.Models;
namespace The_visionaries_Code_404.Services
{
    public interface ICartService
    {
        CartItem GetCartItems(List<int> cartList);
    }
}