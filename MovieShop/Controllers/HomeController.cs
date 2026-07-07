using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using The_visionaries_Code_404.Models;
using The_visionaries_Code_404.Services;

namespace The_visionaries_Code_404.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMovieService _movieService;
        private readonly ICustomerService _customerService;

        public HomeController(ILogger<HomeController> logger, IMovieService movieService, ICustomerService customerService)
        {
            _logger = logger;
            _movieService = movieService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();

            homeViewModel.MostPopularMovies = _movieService.GetMostPopularMovies();
            homeViewModel.NewestFiveMovies = _movieService.GetFiveNewestMovies();
            homeViewModel.OldestFiveMovies = _movieService.GetFiveOldestMovies();
            homeViewModel.CheapestFiveMovies = _movieService.GetFiveCheapestMovies();
            homeViewModel.CustomerMostExpensiveOrder = _customerService.CustomerMostExpensiveOrder();

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
