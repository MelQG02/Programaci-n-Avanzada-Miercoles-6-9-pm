using ClaseEF.Data;
using ClaseEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseEF.Controllers;

public class ClinicasController : Controller
{
    private readonly ClaseEfContext _context;

    public ClinicasController(ClaseEfContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Clinicas.OrderBy(c => c.Nombre).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var clinica = await _context.Clinicas.FirstOrDefaultAsync(m => m.Id == id);
        if (clinica is null)
        {
            return NotFound();
        }

        return View(clinica);
    }

    public IActionResult Create()
    {
        return View(new Clinica());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Clinica clinica)
    {
        if (!ModelState.IsValid)
        {
            return View(clinica);
        }

        _context.Add(clinica);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Clinica creada con Entity Framework.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var clinica = await _context.Clinicas.FindAsync(id);
        if (clinica is null)
        {
            return NotFound();
        }

        return View(clinica);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Clinica clinica)
    {
        if (id != clinica.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(clinica);
        }

        try
        {
            _context.Update(clinica);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Clinica actualizada con Entity Framework.";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Clinicas.AnyAsync(e => e.Id == clinica.Id))
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

        var clinica = await _context.Clinicas.FirstOrDefaultAsync(m => m.Id == id);
        if (clinica is null)
        {
            return NotFound();
        }

        return View(clinica);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var clinica = await _context.Clinicas.FindAsync(id);
        if (clinica is not null)
        {
            _context.Clinicas.Remove(clinica);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Clinica eliminada con Entity Framework.";
        }

        return RedirectToAction(nameof(Index));
    }
}
