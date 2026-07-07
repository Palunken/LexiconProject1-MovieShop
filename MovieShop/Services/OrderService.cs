using The_visionaries_Code_404.Models;
using The_visionaries_Code_404.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace The_visionaries_Code_404.Services
{
    public class OrderService : IOrderService
    {
        private readonly ShopDbContext _db;

        public OrderService(ShopDbContext db)
        {
            _db = db;
        }

        public List<OrderViewModel> GetAll()
        {
            var orders = _db.Orders.OrderByDescending(o => o.OrderDate).ToList();

            List<OrderViewModel> orderVMList = new List<OrderViewModel>();


            foreach (Order order in orders)
            {
                OrderViewModel orderVM = new OrderViewModel();

                var orderRows = _db.OrderRows.Where(oR => oR.OrderId == order.Id).AsEnumerable();

                orderVM.OrderDate = order.OrderDate;

                orderVM.CustomerName = _db.Customers
                    .Where(c => c.Id == order.CustomerId)
                    .Select(c => c.FirstName + " " + c.LastName).FirstOrDefault();

                orderVM.TotalCost = orderRows.Select(oR => oR.Price * oR.Quantities).Sum();

                orderVM.Movies = _db.Movies.Join(orderRows, m => m.Id, oR => oR.MovieId,
                    (m, oR) => new OrderMovieViewModel
                    {
                        Title = m.Title,
                        Price = oR.Price,
                        Quantity = oR.Quantities,
                        TotalPrice = oR.Price * oR.Quantities
                    }).ToList();

                orderVMList.Add(orderVM);
            }

            return orderVMList;
        }

        public List<OrderViewModel> GetAllForCustomer(int customerId)
        {
            var customerOrders = _db.Orders.Where(o => o.CustomerId == customerId)
                .OrderByDescending(o => o.OrderDate).ToList();

            List<OrderViewModel> orderVMList = new List<OrderViewModel>();


            foreach (Order order in customerOrders)
            {
                OrderViewModel orderVM = new OrderViewModel();

                var orderRows = _db.OrderRows.Where(oR => oR.OrderId == order.Id).AsEnumerable();

                orderVM.OrderDate = order.OrderDate;
                orderVM.TotalCost = orderRows.Select(oR => oR.Price * oR.Quantities).Sum();

                orderVM.Movies = _db.Movies.Join(orderRows, m => m.Id, oR => oR.MovieId,
                    (m, oR) => new OrderMovieViewModel
                    {
                        Title = m.Title,
                        Price = oR.Price,
                        Quantity = oR.Quantities,
                        TotalPrice = oR.Price * oR.Quantities
                    }).ToList();

                orderVMList.Add(orderVM);
            }

            orderVMList[0].CustomerName = _db.Customers
                .Where(c => c.Id == customerId)
                .Select(c => c.FirstName + " " + c.LastName).FirstOrDefault();


            return orderVMList;
        }
    }
}