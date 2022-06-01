using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Examen3.Models;

namespace Examen3.Controllers;

public class CatalogosController : Controller
{
    private readonly ILogger<CatalogosController> _logger;

    public CatalogosController(ILogger<CatalogosController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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
