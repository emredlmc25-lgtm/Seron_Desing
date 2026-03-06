using Microsoft.AspNetCore.Mvc;
using SeronDesign.Models;
using Microsoft.EntityFrameworkCore;

namespace SeronDesign.Controllers
{
    public class CustomDesignsController : Controller
    {
        private readonly SeronDesignDbContext _context;

        public CustomDesignsController(SeronDesignDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var designs = await _context.CustomDesigns.ToListAsync();
            return View(designs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CustomDesign design)
        {
            if (ModelState.IsValid)
            {
                _context.Add(design);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(design);
        }
    }
}