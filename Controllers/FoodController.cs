using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Examen3.Data;
using HarryPotter.Models;

namespace Examen3.Controllers
{
    public class FoodController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IWebHostEnvironment _hostEnvironment;

        public FoodController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment=hostEnvironment;
        }

        // GET: Food
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Food.Include(f => f.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Food/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Food/Create
        public IActionResult Create()
        {
            ViewData["IdCategory"] = new SelectList(_context.Category, "Id", "Nombre");
            return View();
        }

        // POST: Food/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,IdCategory,Description,Precio,UrlImage")] Food food)
        {
             //if (ModelState.IsValid)
            //{
                string rutaPrincipal=_hostEnvironment.WebRootPath;
                var archivos=HttpContext.Request.Form.Files;
                if(archivos.Count()>0){
                    string nombreArchivo=Guid.NewGuid().ToString();
                    var subidas=Path.Combine(rutaPrincipal,@"imagenes\platillos\");
                    var extension=Path.GetExtension(archivos[0].FileName);
                    using(var fileStream=new FileStream(Path.Combine(subidas,nombreArchivo + extension),FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStream);
                    }
                    food.UrlImage=@"imagenes\platillos\"+nombreArchivo+extension;

                }
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["IdCategory"] = new SelectList(_context.Category, "Id", "Nombre", food.IdCategory);
            return View(food);
        }

        // GET: Food/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var food = await _context.Food.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "Id", "Nombre", food.IdCategory);
            return View(food);
        }

        // POST: Food/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,IdCategory,Description,Precio,UrlImage")] Food food)
        {
            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string rutaPrincipal=_hostEnvironment.WebRootPath;
                    var archivos=HttpContext.Request.Form.Files;
                    if(archivos.Count()>0){
                        if(food!=null && food.UrlImage!=null){
                            var rutaImagenActual= Path.Combine(rutaPrincipal,food.UrlImage);
                            if(System.IO.File.Exists(rutaImagenActual)){
                                System.IO.File.Delete(rutaImagenActual);
                            }
                            _context.Entry(food).State=EntityState.Detached;
                        }
                        string nombreArchivo=Guid.NewGuid().ToString();
                        var subidas=Path.Combine(rutaPrincipal,@"imagenes\platillos\");
                        var extension=Path.GetExtension(archivos[0].FileName);
                        using(var fileStream=new FileStream(Path.Combine(subidas,nombreArchivo + extension),FileMode.Create))
                        {
                            archivos[0].CopyTo(fileStream);
                        }
                        food.UrlImage=@"imagenes\platillos\"+nombreArchivo+extension;
                        _context.Entry(food).State=EntityState.Modified;

                    }
                    
                    _context.Update(food);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategory"] = new SelectList(_context.Category, "Id", "Nombre", food.IdCategory);
            return View(food);
        }

        // GET: Food/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Food == null)
            {
                return NotFound();
            }

            var food = await _context.Food
                .Include(f => f.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // POST: Food/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Food == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Food'  is null.");
            }
            var food = await _context.Food.FindAsync(id);
            if (food != null)
            {
                _context.Food.Remove(food);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodExists(int id)
        {
          return (_context.Food?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
