using dgPadCmsNew.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicWebsite.Controllers
{
    public class ArtController : Controller
    {
        private readonly dgPadCmsNewContext _db;

        public ArtController(dgPadCmsNewContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View((_db.Taxonomies.ToList(), _db.Posts.ToList()));
        }
    }
}

