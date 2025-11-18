using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // get brands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        // get brands/create Show Create Form
        public IActionResult Create()
        {
            return View();
        }

        // post brands/create
        [HttpPost]  //for submission 
        [ValidateAntiForgeryToken] // block for the request if their is no token
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid) ////Validates model,
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // get brands/edit/id
        public async Task<IActionResult> Edit(int id) //search bby id baaden it pass it to view to edit it
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        // poat brands/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

        // get brands/delete/id
        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        // post brands/delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
