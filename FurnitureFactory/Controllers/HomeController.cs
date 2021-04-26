using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FurnitureFactory.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FurnitureFactory.Models;

namespace FurnitureFactory.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FurnitureFactoryDbContext _dbContext;
        public HomeController(ILogger<HomeController> logger, FurnitureFactoryDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Modules.OrderBy(item => item.Name).ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}