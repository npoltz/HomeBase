using HomeBase.Core;
using HomeBase.Web.Models;
using HomeBase.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace HomeBase.Web.Controllers
{
    public class HomeController : Controller
    {
        private const string SENSOR_ID = "s01";
        private readonly IDataLogServices _services;
        private readonly SensorDataViewModel _viewModel;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            ILogger<HomeController> logger,
            IDataLogServices services
            )
        {
            _logger = logger;
            _services = services;
            _viewModel = new SensorDataViewModel
            {
                Periodicity = Periodicity.Unknown,
                JsonDataLogs = _services.GetDataLogs(SENSOR_ID, Periodicity.Unknown).Result
            };
        }

        public IActionResult Index()
        {
            return View(_viewModel);
        }

        public IActionResult SetPeriodicity(int periodicity)
        {
            var jsonDataLogs = _services.GetDataLogs(SENSOR_ID, (Periodicity)periodicity).Result;
            return new ContentResult { Content = jsonDataLogs, ContentType = "application/json" };
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
