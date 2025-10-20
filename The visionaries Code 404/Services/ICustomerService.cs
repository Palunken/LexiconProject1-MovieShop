using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using The_visionaries_Code_404.Data;

namespace The_visionaries_Code_404.Services
{
    public interface ICustomerService
    {
        public void Create();
        public void Create(Customer customer);
        public List<Customer> GetAllCustomers();
        public Customer GetCustomerById(int id);
        public Customer CustomerMostExpensiveOrder();
        public void UpdateCustomer(Customer customer);
    }
}
