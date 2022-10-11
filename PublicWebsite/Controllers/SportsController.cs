using dgPadCmsNew.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicWebsite.Controllers
{
    public class SportsController : Controller
    {
        private readonly dgPadCmsNewContext _db;

        public SportsController( dgPadCmsNewContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View((_db.Taxonomies.ToList(), _db.Posts.ToList()));
        }
    }
}
