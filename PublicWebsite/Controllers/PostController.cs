using dgPadCmsNew.Infrastructure;
using dgPadCmsNew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicWebsite.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly dgPadCmsNewContext context;

        public PostController(dgPadCmsNewContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index(PostType postType)
        {
            var posts = context.Posts.OrderByDescending(x => x.CreationDate);

            return View(await posts.ToListAsync());
        }
    }
}
