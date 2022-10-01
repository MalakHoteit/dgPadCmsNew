using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dgPadCmsNew.Infrastructure;
using dgPadCmsNew.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dgPadCmsNew.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly dgPadCmsNewContext context;

        public PostsController(dgPadCmsNewContext context)
        {
            this.context = context;
        }

        // GET /posts
        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            var products = context.Posts.OrderByDescending(x => x.Id)
                                            .Skip((p - 1) * pageSize)
                                            .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Posts.Count() / pageSize);

            return View(await products.ToListAsync());
        }

        // GET /posts/postType
        public async Task<IActionResult> PostsByPostTypes(string postTypeTitle, int p = 1)
        {
            PostType postType = await context.PostTypes.Where(x => x.Title == postTypeTitle).FirstOrDefaultAsync();
            if (postType == null) return RedirectToAction("Index");

            int pageSize = 6;
            var posts = context.Posts.OrderByDescending(x => x.Id)
                                            .Where(x => x.PostTypeId == postType.PostTypeId)
                                            .Skip((p - 1) * pageSize)
                                            .Take(pageSize);

            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)context.Posts.Where(x => x.PostTypeId == postType.PostTypeId).Count() / pageSize);
            ViewBag.PostTypeTitle = postType.Title;
            ViewBag.PostTypeCode = postTypeTitle;

            return View(await posts.ToListAsync());
        }
    }
}