using dgPadCmsNew.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PublicWebsite.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PublicWebsite.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly dgPadCmsNewContext _db;

        public HomeController(ILogger<HomeController> logger, dgPadCmsNewContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View((_db.Taxonomies.ToList(), _db.Posts.ToList()));
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
