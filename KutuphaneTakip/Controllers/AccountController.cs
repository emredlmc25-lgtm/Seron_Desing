using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeronDesign.Models;
using SeronDesign.ViewModels;

namespace SeronDesign.Controllers;

public class AccountController : Controller
{
private readonly ILogger<AccountController> _logger;
private readonly SeronDesignDbContext _context;


public AccountController(ILogger<AccountController> logger, SeronDesignDbContext context)
{
    _logger = logger;
    _context = context;
}

public IActionResult Register()
{
    return View(new RegisterViewModels());
}

[HttpPost]
public async Task<IActionResult> RegisterAsync(RegisterViewModels model)
{
    if (ModelState.IsValid == false)
    {
        return View(model);
    }

    var user = new User()
    {
        FirstName = model.FirstName,
        LastName = model.LastName,
        Email = model.Email,
        Password = model.Password,
        Role = 1 // Assuming 1 for user
    };

    await _context.Users.AddAsync(user);
    
    await _context.SaveChangesAsync();

    return RedirectToAction("Login");
}

public IActionResult Login()
{
    return View(new LoginViewModel());
}

[HttpPost]
public async Task<IActionResult> Login(LoginViewModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }

    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

    if (user == null)
    {
        ModelState.AddModelError(string.Empty, "Invalid email or password.");
        return View(model);
    }

    TempData["UserId"] = user.Id;
    TempData["UserName"] = user.FirstName;

    return RedirectToAction("Index", "Home");
}

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public IActionResult Error()
{
    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}


}
