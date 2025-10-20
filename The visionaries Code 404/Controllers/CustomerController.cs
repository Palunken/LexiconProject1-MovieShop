using System.ComponentModel.DataAnnotations;
using The_visionaries_Code_404.Data;
using The_visionaries_Code_404.Models;
using The_visionaries_Code_404.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using The_visionaries_Code_404.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace The_visionaries_Code_404.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ShopDbContext _db;
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        
        public CustomerController( ICustomerService customerService, IOrderService orderService, ShopDbContext db)
        {
            _db= db;
            _customerService = customerService;
            _orderService = orderService;
        }

        [Route("Customers")]
        public IActionResult Index()
        {
            var customers = _customerService.GetAllCustomers();

            return View(customers);
        }

        public IActionResult CustomerDetails(int id)
        {
            var customer = _customerService.GetCustomerById(id);

            return View(customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            TempData["CustomerEmail"] = HttpContext.Session.GetString("RegisteredEmail") ?? "";
            if (HttpContext.Session.Get<Customer>("customerSession") != null)
            {
                return View(HttpContext.Session.Get<Customer>("customerSession"));
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var customerFromDB = _db.Customers.FirstOrDefault(m => m.Email == customer.Email);
                if (customerFromDB == null)
                {
                    // Creates customer, register in database and redirects to cart
                    HttpContext.Session.SetString("customerSession", JsonConvert.SerializeObject(customer));
                    _customerService.Create(customer);
                    TempData["CustomerMessage"] = $"{customer.Email} was added as a customer.";
                    HttpContext.Session.SetString("RegisteredEmail", customer.Email);
                    HttpContext.Session.Remove("customerSession");
                    return RedirectToAction("ShoppingCart", "Cart");
                }
                // Notifies if email is already registered
                TempData["CustomerCreateMessage"] = $"{customer.Email} already exist in the database. Please enter another email.";
                customer.Email = HttpContext.Session.GetString("RegisteredEmail") ?? "";
                HttpContext.Session.SetString("customerSession", JsonConvert.SerializeObject(customer));
                return RedirectToAction("Create");

            }
            TempData["CustomerCreateMessage"] = "Something went wrong. Customer not registered.";
            return RedirectToAction("Create");
        }

        public IActionResult EditCustomer(int id)
        {
            var customer = _customerService.GetCustomerById(id);

            return View(customer);
        }

        [HttpPost]
        public IActionResult EditCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomer(customer);
                TempData["CustomerEditMessage"] = $"{customer.FirstName} {customer.LastName} has been updated.";
            }
            else
                TempData["CustomerEditMessage"] = "Something went wrong.";

            return RedirectToAction("CustomerUpdated");
        }

        public IActionResult CustomerUpdated()
        {
            return View();
        }

        public IActionResult AllOrdersOneCustomer(int id)
        {
            List<OrderViewModel> ordersVM = _orderService.GetAllForCustomer(id);
            ViewBag.CustomerName = ordersVM.First().CustomerName;
            return View(ordersVM);
        }

        public IActionResult AllOrdersEveryCustomer()
        {
            List<OrderViewModel> ordersVM = _orderService.GetAll();

            return View(ordersVM);
        }


    }
}
