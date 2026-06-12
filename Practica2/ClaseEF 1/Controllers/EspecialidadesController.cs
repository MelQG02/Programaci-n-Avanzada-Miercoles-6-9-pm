using ClaseEF.Data;
using ClaseEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseEF.Controllers;

public class EspecialidadesController : Controller
{
    private readonly ClaseEfContext _context;

    public EspecialidadesController(ClaseEfContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Especialidades.OrderBy(e => e.Nombre).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidades.FirstOrDefaultAsync(m => m.Id == id);
        if (especialidad is null)
        {
            return NotFound();
        }

        return View(especialidad);
    }

    public IActionResult Create()
    {
        return View(new Especialidade());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Especialidade especialidad)
    {
        if (!ModelState.IsValid)
        {
            return View(especialidad);
        }

        _context.Add(especialidad);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Especialidad creada con Entity Framework.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidades.FindAsync(id);
        if (especialidad is null)
        {
            return NotFound();
        }

        return View(especialidad);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Especialidade especialidad)
    {
        if (id != especialidad.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(especialidad);
        }

        try
        {
            _context.Update(especialidad);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Especialidad actualizada con Entity Framework.";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Especialidades.AnyAsync(e => e.Id == especialidad.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidades.FirstOrDefaultAsync(m => m.Id == id);
        if (especialidad is null)
        {
            return NotFound();
        }

        return View(especialidad);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var especialidad = await _context.Especialidades.FindAsync(id);
        if (especialidad is not null)
        {
            _context.Especialidades.Remove(especialidad);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Especialidad eliminada con Entity Framework.";
        }

        return RedirectToAction(nameof(Index));
    }
}
