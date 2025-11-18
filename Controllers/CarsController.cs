using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;


namespace WebApplication1.Controllers
{

    public class CarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var cars = _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .Include(c => c.Supplier);

            return View(await cars.ToListAsync());
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Brands.ToList(), "Id", "Name");
            ViewData["CategoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Categories.ToList(), "Id", "Name");
            ViewData["SupplierId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Suppliers.ToList(), "Id", "Name");
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Car car)
        {
            // Remove validation errors for navigation properties since we only need the IDs
            ModelState.Remove("Brand");
            ModelState.Remove("Category");
            ModelState.Remove("Supplier");

            // Validate that IDs are greater than 0
            if (car.BrandId <= 0)
            {
                ModelState.AddModelError("BrandId", "The Brand field is required.");
            }
            if (car.CategoryId <= 0)
            {
                ModelState.AddModelError("CategoryId", "The Category field is required.");
            }
            if (car.SupplierId <= 0)
            {
                ModelState.AddModelError("SupplierId", "The Supplier field is required.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(car);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while saving: {ex.Message}");
                }
            }

            ViewData["BrandId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Brands.ToList(), "Id", "Name", car?.BrandId ?? 0);
            ViewData["CategoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Categories.ToList(), "Id", "Name", car?.CategoryId ?? 0);
            ViewData["SupplierId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Suppliers.ToList(), "Id", "Name", car?.SupplierId ?? 0);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            ViewData["BrandId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Brands.ToList(), "Id", "Name", car.BrandId);
            ViewData["CategoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Categories.ToList(), "Id", "Name", car.CategoryId);
            ViewData["SupplierId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Suppliers.ToList(), "Id", "Name", car.SupplierId);

            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car)
        {
            if (id != car.Id) return NotFound();

            // Remove validation errors for navigation properties since we only need the IDs
            ModelState.Remove("Brand");
            ModelState.Remove("Category");
            ModelState.Remove("Supplier");

            // Validate that IDs are greater than 0
            if (car.BrandId <= 0)
            {
                ModelState.AddModelError("BrandId", "The Brand field is required.");
            }
            if (car.CategoryId <= 0)
            {
                ModelState.AddModelError("CategoryId", "The Category field is required.");
            }
            if (car.SupplierId <= 0)
            {
                ModelState.AddModelError("SupplierId", "The Supplier field is required.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while saving: {ex.Message}");
                }
            }

            ViewData["BrandId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Brands.ToList(), "Id", "Name", car?.BrandId ?? 0);
            ViewData["CategoryId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Categories.ToList(), "Id", "Name", car?.CategoryId ?? 0);
            ViewData["SupplierId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Suppliers.ToList(), "Id", "Name", car?.SupplierId ?? 0);
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var car = await _context.Cars
                .Include(c => c.Brand)
                .Include(c => c.Category)
                .Include(c => c.Supplier)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (car == null) return NotFound();

            return View(car);
        }

        // POST: Cars/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
