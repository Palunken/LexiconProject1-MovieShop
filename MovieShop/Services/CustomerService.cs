using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;
using The_visionaries_Code_404.Data;


namespace The_visionaries_Code_404.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ShopDbContext _db;

        public CustomerService(ShopDbContext db)
        {
            _db = db;
        }

        public void Create()
        {
            //throw new NotImplementedException();
        }

        public void Create(Customer customer)
        {
            _db.Customers.Add(customer);
            _db.SaveChanges();
            
        }

        public List<Customer> GetAllCustomers()
        {
            if (_db.Customers.Any())
                return _db.Customers.OrderBy(c => c.LastName).ToList();
            else
                return new List<Customer>();
        }

        public Customer GetCustomerById(int id)
        {
            var customer = _db.Customers.Where(c => c.Id == id).FirstOrDefault();

            if (customer != null)
                return customer;
            else
                throw new Exception("No such customer");
        }
        public Customer CustomerMostExpensiveOrder()
        {
            var mostExpensiveOrder_Id = _db.OrderRows.GroupBy(oR => oR.OrderId)
                .Select(g => new
                {
                    OrderId = g.Key,
                    TotalPrice = g.Sum(r => r.Price)
                }).OrderByDescending(g => g.TotalPrice).Select(g => g.OrderId).FirstOrDefault();

            var customer = _db.Customers.Where(c => c.Orders.Any(o => o.Id == mostExpensiveOrder_Id)).FirstOrDefault();

            return customer;
        }
        
        public void UpdateCustomer(Customer customer)
        {
            _db.Customers.Update(customer);
            _db.SaveChanges();
        }
    }
}
