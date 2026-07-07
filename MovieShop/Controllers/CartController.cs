using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using The_visionaries_Code_404.Data;
using The_visionaries_Code_404.Extensions;
using The_visionaries_Code_404.Models;
using The_visionaries_Code_404.Services;

namespace The_visionaries_Code_404.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ShopDbContext _db;
        public CartController(ICartService cartService, ShopDbContext db)
        {
            _cartService = cartService;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            var cartList = HttpContext.Session.Get<List<int>>("ShoppingCart") ?? new List<int>();
            var cartMovies = _cartService.GetCartItems(cartList);
            TempData["CustomerEmail"] = HttpContext.Session.GetString("RegisteredEmail") ?? "";
            return View(cartMovies);
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var cartList = HttpContext.Session.Get<List<int>>("ShoppingCart") ?? new List<int>();
            cartList.Add(id);
            var numberOfListItems = cartList.Count();
            HttpContext.Session.Set<List<int>>("ShoppingCart", cartList);
            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cartList = HttpContext.Session.Get<List<int>>("ShoppingCart") ?? new List<int>();
            foreach (var item in cartList)
            {
                if (item == id)
                {
                    cartList.Remove(item);
                    break;
                }
            }
            var numberOfListItems = cartList.Count();
            HttpContext.Session.Set<List<int>>("ShoppingCart", cartList);
            return RedirectToAction("ShoppingCart");
        }

        [HttpPost]
        public IActionResult Checkout(string email)
        {
            var cartList = HttpContext.Session.Get<List<int>>("ShoppingCart") ?? new List<int>();

            if (!cartList.Any())
            {
                ModelState.AddModelError("", "Your cart is empty.");
                return RedirectToAction("ShoppingCart");
            }

            var customer = _db.Customers.FirstOrDefault(c => c.Email == email);

            if (customer == null)
            {
                TempData["CustomerCreateMessage"] = "Email address not found. Please register an account.";
                HttpContext.Session.SetString("RegisteredEmail", email);
                return RedirectToAction("Create", "Customer");
            }

            var cartMovies = _cartService.GetCartItems(cartList);

            var order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now,
                OrderRows = cartMovies.MovieList.Select(m => new OrderRow
                {
                    MovieId = m.Id,
                    Quantities = cartList.Count(id => id == m.Id),
                    Price = m.Price
                }).ToList()
            };

            _db.Orders.Add(order);
            _db.SaveChanges();

            HttpContext.Session.Remove("ShoppingCart");
            HttpContext.Session.Remove("RegisteredEmail");
            var orderSummary = new OrderSummary
            {
                Customer = customer,
                PurchasedMovies = cartMovies.MovieList,
                TotalPrice = cartMovies.TotalPrice,
                CartList = cartMovies.CartList,
            };

            return View("Success", orderSummary);
        }
    }
}