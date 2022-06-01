using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Examen3.Models;
using HarryPotter.Models;
using Examen3.Data;
using Microsoft.EntityFrameworkCore;

namespace Examen3.Controllers;

public class MenuController : Controller
{
    private readonly ILogger<MenuController> _logger;

    private readonly ApplicationDbContext _context;

    public MenuController(ILogger<MenuController> logger,ApplicationDbContext context)
    {
        _logger = logger;
        _context=context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Postres()
    {

        var applicationDbContext = _context.Food.Include(f => f.Category);
        return View(await applicationDbContext.ToListAsync());
    }

    public async Task<IActionResult> Bebidas()
    {

        var applicationDbContext = _context.Food.Include(f => f.Category);
        return View(await applicationDbContext.ToListAsync());
    }

    public async Task<IActionResult> Comidas()
    {

        var applicationDbContext = _context.Food.Include(f => f.Category);
        return View(await applicationDbContext.ToListAsync());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
