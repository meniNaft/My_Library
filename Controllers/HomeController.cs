using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using My_Library.DAL;
using My_Library.Models;
using System.Diagnostics;

namespace My_Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext dataContext;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            dataContext = context;
        }

        public IActionResult Index()
        {

            return View();
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

        public IActionResult GetModalData()
        {
            var data = new
            {
                Shelves = dataContext.Shelves.Select(s => new { s.Id, s.Name }).ToList(),
                Sets = dataContext.Sets.Select(s => new { s.Id, s.Name }).ToList(),
                Genres = dataContext.Genres.Select(g => new { g.Id, g.Name }).ToList()
            };

            return Json(data);
        }
    }
}
