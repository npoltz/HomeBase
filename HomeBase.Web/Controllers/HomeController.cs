using HomeBase.Web.Models;
using HomeBase.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HomeBase.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataLogServices _services;

        public HomeController(
            ILogger<HomeController> logger,
            IDataLogServices services
            )
        {
            _logger = logger;
            _services = services;
        }

        public IActionResult Index()
        {
            return View(new SensorDataViewModel(_services, "s01"));
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
