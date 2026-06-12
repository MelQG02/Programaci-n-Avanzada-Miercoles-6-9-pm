using ClaseEF.Data;
using ClaseEF.Models;
using ClaseEF.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClaseEF.Controllers;

public class ServiciosController : Controller
{
    private readonly ClaseEfContext _context;

    public ServiciosController(ClaseEfContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Servicios.OrderBy(s => s.Nombre).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var servicio = await _context.Servicios.FirstOrDefaultAsync(m => m.Id == id);
        if (servicio is null)
        {
            return NotFound();
        }

        ViewBag.EspecialidadNombre = await _context.Especialidades
            .Where(e => e.Id == servicio.Especialidad)
            .Select(e => e.Nombre)
            .FirstOrDefaultAsync() ?? "Sin especialidad";

        return View(servicio);
    }

    public async Task<IActionResult> Create()
    {
        return View(await BuildViewModelAsync(new Servicio
        {
            FechaDeRegistro = DateTime.Now,
            ESTADO = true
        }));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServicioFormViewModel viewModel)
    {
        if (!await _context.Especialidades.AnyAsync())
        {
            ModelState.AddModelError("Servicio.Especialidad", "Primero crea al menos una especialidad.");
        }

        if (!ModelState.IsValid)
        {
            return View(await BuildViewModelAsync(viewModel.Servicio));
        }

        viewModel.Servicio.FechaDeRegistro = DateTime.Now;
        _context.Add(viewModel.Servicio);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Servicio creado con Entity Framework.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var servicio = await _context.Servicios.FindAsync(id);
        if (servicio is null)
        {
            return NotFound();
        }

        return View(await BuildViewModelAsync(servicio));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ServicioFormViewModel viewModel)
    {
        if (id != viewModel.Servicio.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(await BuildViewModelAsync(viewModel.Servicio));
        }

        var servicioDb = await _context.Servicios.FindAsync(id);
        if (servicioDb is null)
        {
            return NotFound();
        }

        servicioDb.Nombre = viewModel.Servicio.Nombre;
        servicioDb.Descripcion = viewModel.Servicio.Descripcion;
        servicioDb.Monto = viewModel.Servicio.Monto;
        servicioDb.IVA = viewModel.Servicio.IVA;
        servicioDb.Especialidad = viewModel.Servicio.Especialidad;
        servicioDb.Especialista = viewModel.Servicio.Especialista;
        servicioDb.Clinica = viewModel.Servicio.Clinica;
        servicioDb.ESTADO = viewModel.Servicio.ESTADO;
        servicioDb.FechaDeModificacion = DateTime.Now;

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Servicio actualizado con Entity Framework.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var servicio = await _context.Servicios.FirstOrDefaultAsync(m => m.Id == id);
        if (servicio is null)
        {
            return NotFound();
        }

        return View(servicio);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var servicio = await _context.Servicios.FindAsync(id);
        if (servicio is not null)
        {
            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Servicio eliminado con Entity Framework.";
        }

        return RedirectToAction(nameof(Index));
    }

    private async Task<ServicioFormViewModel> BuildViewModelAsync(Servicio servicio)
    {
        return new ServicioFormViewModel
        {
            Servicio = servicio,
            Especialidades = await _context.Especialidades
                .OrderBy(e => e.Nombre)
                .Select(e => new SelectListItem
                {
                    Value = e.Id.ToString(),
                    Text = e.Nombre
                })
                .ToListAsync(),
            ClinicasExistentes = await _context.Clinicas
                .OrderBy(c => c.Nombre)
                .Select(c => c.Nombre)
                .ToListAsync()
        };
    }
}
