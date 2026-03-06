using Microsoft.AspNetCore.Mvc;
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
            var toys = await _context.Toys.Include(t => t.Category).ToListAsync();
            return View(toys);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Toy toy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(toy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Categories = _context.Categories.ToList();
            return View(toy);
        }
    }
}