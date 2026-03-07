public IActionResult Create()
{
    ViewBag.Categories = _context.Categories.ToList();
    return View();
}

[HttpPost]
public async Task<IActionResult> Create(Product product)
{
    if (ModelState.IsValid)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    ViewBag.Categories = _context.Categories.ToList();
    return View(product);
}