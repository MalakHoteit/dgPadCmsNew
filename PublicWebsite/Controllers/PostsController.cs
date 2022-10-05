using dgPadCmsNew.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicWebsite.Controllers
{
    public class PostsController : Controller
    {
        private readonly dgPadCmsNewContext context;

        public PostsController(dgPadCmsNewContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            
            return View(await context.Posts.OrderByDescending(x => x.CreationDate).Include(x => x.PostType).ToListAsync());

        }
    }
    }
