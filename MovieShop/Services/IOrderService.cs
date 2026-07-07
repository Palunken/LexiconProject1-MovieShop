using The_visionaries_Code_404.Data;
using The_visionaries_Code_404.Models;

namespace The_visionaries_Code_404.Services
{
    public interface IOrderService
    {
        public List<OrderViewModel> GetAll();
        public List<OrderViewModel> GetAllForCustomer(int customerId);
    }
}
