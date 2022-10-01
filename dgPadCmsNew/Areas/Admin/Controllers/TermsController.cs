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
    public class TermsController : Controller
    {
        private readonly dgPadCmsNewContext context;

        public TermsController(dgPadCmsNewContext context)
        {
            this.context = context;
        }

        // GET /admin/terms
        public async Task<IActionResult> Index()
        {
            return View(await context.Term.OrderByDescending(x => x.Id).Include(x => x.Taxonomy).ToListAsync());
        }

        // GET /admin/terms/create
        public IActionResult Create()
        {
            ViewBag.TaxonomyId = new SelectList(context.Taxonomies.OrderBy(x => x.Code), "TaxonomyId", "Name");

            return View();
        }


        // POST /admin/terms/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Term term)
        {
            if (ModelState.IsValid)
            {
                term.code = term.Name.ToLower().Replace(" ", "-");


                var code = await context.Term.FirstOrDefaultAsync(x => x.code == term.code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The term already exists.");
                    return View(term);
                }

                context.Add(term);
                await context.SaveChangesAsync();

                TempData["Success"] = "The term has been added!";

                return RedirectToAction("Index");
            }

            return View(term);
        }

        // GET /admin/terms/edit/5
        public async Task<IActionResult> Edit(int id)
        {
            Term term = await context.Term.FindAsync(id);
            if (term == null)
            {
                return NotFound();
            }

            return View(term);
        }

        // POST /admin/terms/edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Term term)
        {
            if (ModelState.IsValid)
            {
                term.code = term.Name.ToLower().Replace(" ", "-");

                var code = await context.Term.Where(x => x.Id != id).FirstOrDefaultAsync(x => x.code == term.code);
                if (code != null)
                {
                    ModelState.AddModelError("", "The term already exists.");
                    return View(term);
                }

                context.Update(term);
                await context.SaveChangesAsync();

                TempData["Success"] = "The term has been edited!";

                return RedirectToAction("Edit", new { id });
            }

            return View(term);
        }

        // GET /admin/terms/delete/5
        public async Task<IActionResult> Delete(int id)
        {
            Term term = await context.Term.FindAsync(id);

            if (term == null)
            {
                TempData["Error"] = "The term does not exist!";
            }
            else
            {
                context.Term.Remove(term);
                await context.SaveChangesAsync();

                TempData["Success"] = "The term has been deleted!";
            }

            return RedirectToAction("Index");
        }

        // POST /admin/pages/terms
        [HttpPost]
        public async Task<IActionResult> Reorder(int[] id)
        {
            int count = 1;

            foreach (var termId in id)
            {
                Term term = await context.Term.FindAsync(termId);

                context.Update(term);
                await context.SaveChangesAsync();
                count++;
            }

            return Ok();
        }
    }
}
