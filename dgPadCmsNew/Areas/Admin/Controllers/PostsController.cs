using dgPadCmsNew.Infrastructure;
using dgPadCmsNew.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dgPadCmsNew.Areas.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly dgPadCmsNewContext context;

        public PostsController(dgPadCmsNewContext context)
        {
            this.context = context;
        }

        // GET /admin/posts
        public async Task<IActionResult> Index()
        {
            return View(await context.Posts.OrderByDescending(x => x.Id).Include(x =>x.PostType).ToListAsync());
            
        }

        

        // GET /admin/posts/create
        public IActionResult Create()
        {
            ViewBag.PostTypeId = new SelectList(context.PostTypes.OrderBy(x => x.Code), "PostTypeId", "Title");

            return View();
        }

        // POST /admin/posts/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.Title = post.Title.ToLower().Replace(" ", "-");


                var Title = await context.Posts.FirstOrDefaultAsync(x => x.Title == post.Title);
                if (Title != null)
                {
                    ModelState.AddModelError("", "The post already exists.");
                    return View(post);
                }

                context.Add(post);
                await context.SaveChangesAsync();

                TempData["Success"] = "The post has been added!";

                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET /admin/posts/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Post post = await context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }


        // POST /admin/posts/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (ModelState.IsValid)
            {
                post.Detail = post.Title.ToLower().Replace(" ", "-");

                var detail = await context.Posts.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.Detail == post.Detail);
                if (detail != null)
                {
                    ModelState.AddModelError("", "The post already exists.");
                    return View(post);
                }

                context.Update(post);
                await context.SaveChangesAsync();

                TempData["Success"] = "The post has been edited!";

                return RedirectToAction("Edit", new { id });
            }

            return View(post);
        }

    }
}
