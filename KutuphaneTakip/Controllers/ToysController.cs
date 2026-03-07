using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeronDesign.Models;
using Microsoft.EntityFrameworkCore;

namespace SeronDesign.Controllers
{
    public class ToysController : Controller
    {
        private readonly SeronDesignDbContext _context;

        public ToysController(SeronDesignDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var toys = await _context.Toys
                .Include(t => t.Category)
                .AsNoTracking()
                .ToListAsync();
            return View(toys);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Toy toy)
        {
            if (ModelState.IsValid)
            {
                _context.Toys.Add(toy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            await LoadCategoriesAsync(toy.CategoryId);
            return View(toy);
        }

        private async Task LoadCategoriesAsync(int? selectedCategoryId = null)
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .OrderBy(c => c.Name)
                .ToListAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedCategoryId);
        }
    }
}
