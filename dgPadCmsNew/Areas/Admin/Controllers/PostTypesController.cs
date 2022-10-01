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
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class PostTypesController : Controller
    {

        private readonly dgPadCmsNewContext context;

        public PostTypesController(dgPadCmsNewContext context)
        {
            this.context = context;
        }
        // GET /admin/postTypes
        public async Task<IActionResult> Index()
        {
            return View(await context.PostTypes.OrderByDescending(x => x.PostTypeId).ToListAsync());
        }

        // GET /admin/postTypes/create
        public IActionResult Create() => View();
        

        // POST /admin/postTypes/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostType postType)
        {
            if (ModelState.IsValid)
            {
                postType.Code = postType.Title.ToLower().Replace(" ", "-");


                var Code = await context.PostTypes.FirstOrDefaultAsync(x => x.Code == postType.Code);
                if (Code != null)
                {
                    ModelState.AddModelError("", "The postType already exists.");
                    return View(postType);
                }

                context.Add(postType);
                await context.SaveChangesAsync();

                TempData["Success"] = "The postType has been added!";

                return RedirectToAction("Index");
            }
        
            return View(postType);
    }

        // GET /admin/postTypes/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            PostType postType = await context.PostTypes.FindAsync(id);
            if (postType == null)
            {
                return NotFound();
            }

            return View(postType);
        }

        // POST /admin/terms/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostType postType)
        {
            if (ModelState.IsValid)
            {
                postType.Code = postType.Title.ToLower().Replace(" ", "-");

                var code = await context.PostTypes.Where(x => x.PostTypeId != id).FirstOrDefaultAsync(x => x.Code == postType.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The postType already exists.");
                    return View(postType);
                }

                context.Update(postType);
                await context.SaveChangesAsync();

                TempData["Success"] = "The postType has been edited!";

                return RedirectToAction("Edit", new { id });
            }

            return View(postType);
        }

        // GET /admin/postTypes/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            PostType postType = await context.PostTypes.FindAsync(id);

            if (postType == null)
            {
                TempData["Error"] = "The postType does not exist!";
            }
            else
            {
                context.PostTypes.Remove(postType);
                await context.SaveChangesAsync();

                TempData["Success"] = "The postType has been deleted!";
            }

            return RedirectToAction("Index");
        }

        // POST /admin/pages/postTypes
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;

            foreach (var PostTypeId in id)
            {
                PostType postType = await context.PostTypes.FindAsync(PostTypeId);

                context.Update(postType);
                await context.SaveChangesAsync();
                count++;
            }

            return Ok();
        }
    }
}
