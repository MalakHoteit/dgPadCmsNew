using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dgPadCmsNew.Infrastructure;
using dgPadCmsNew.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace dgPadCmsNew.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class TaxonomiesController : Controller
    {

        private readonly dgPadCmsNewContext context;

        public TaxonomiesController(dgPadCmsNewContext context)
        {
            this.context = context;
        }

        // GET /admin/taxonomies
        public async Task<IActionResult> Index()
        {
            return View(await context.Taxonomies.OrderBy(x => x.Code).ToListAsync());
        }

        // GET /admin/taxonomies/create
        public IActionResult Create() => View();

        // POST /admin/taxonomies/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Taxonomy taxonomy)
        {
            if (ModelState.IsValid)
            {
                taxonomy.Code = taxonomy.Name.ToLower().Replace(" ", "-");


                var code = await context.Taxonomies.FirstOrDefaultAsync(x => x.Code == taxonomy.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The taxonomy already exists.");
                    return View(taxonomy);
                }

                context.Add(taxonomy);
                await context.SaveChangesAsync();

                TempData["Success"] = "The taxonomy has been added!";

                return RedirectToAction("Index");
            }

            return View(taxonomy);
        }

        // GET /admin/taxonomies/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Taxonomy taxonomy = await context.Taxonomies.FindAsync(id);
            if (taxonomy == null)
            {
                return NotFound();
            }

            return View(taxonomy);
        }

        // POST /admin/taxonomies/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Taxonomy taxonomy)
        {
            if (ModelState.IsValid)
            {
                taxonomy.Code = taxonomy.Name.ToLower().Replace(" ", "-");

                var code = await context.Taxonomies.Where(x => x.TaxonomyId != id).FirstOrDefaultAsync(x => x.Code == taxonomy.Code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The taxonomy already exists.");
                    return View(taxonomy);
                }

                context.Update(taxonomy);
                await context.SaveChangesAsync();

                TempData["Success"] = "The taxonomy has been edited!";

                return RedirectToAction("Edit", new { id });
            }

            return View(taxonomy);
        }

        // GET /admin/taxonomies/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Taxonomy taxonomy = await context.Taxonomies.FindAsync(id);

            if (taxonomy == null)
            {
                TempData["Error"] = "The taxonomy does not exist!";
            }
            else
            {
                context.Taxonomies.Remove(taxonomy);
                await context.SaveChangesAsync();

                TempData["Success"] = "The taxonomy has been deleted!";
            }

            return RedirectToAction("Index");
        }

        // POST /admin/pages/taxonomies
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;

            foreach (var taxonomyId in id)
            {
                Taxonomy taxonomy = await context.Taxonomies.FindAsync(taxonomyId);

                context.Update(taxonomy);
                await context.SaveChangesAsync();
                count++;
            }

            return Ok();
        }
    }
}