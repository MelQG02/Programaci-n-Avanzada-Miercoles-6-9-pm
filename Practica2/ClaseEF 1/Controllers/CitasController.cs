using ClaseEF.Data;
using ClaseEF.Models;
using ClaseEF.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClaseEF.Controllers;

public class CitasController : Controller
{
    private readonly ClaseEfContext _context;

    public CitasController(ClaseEfContext context)
    {
        _context = context;
    }

    // Lista las citas ordenadas por fecha de la cita, mostrando el
    // nombre del servicio y el nombre del paciente asociados a cada cita
    public async Task<IActionResult> Index()
    {
        var servicios = await _context.Servicios.ToDictionaryAsync(s => s.Id, s => s.Nombre);
        var pacientes = await _context.Pacientes.ToDictionaryAsync(p => p.Id, p => p.NombreDeLaPersona);

        var model = await _context.CITAs
            .OrderByDescending(c => c.FechaDeLaCita)
            .Select(c => new CitaListItemViewModel
            {
                Cita = c
            })
            .ToListAsync();

        foreach (var item in model)
        {
            item.NombreServicio = servicios.TryGetValue(item.Cita.IdServicio, out var nombre)
                ? nombre
                : "Sin servicio";

            item.NombrePaciente = pacientes.TryGetValue(item.Cita.IdPaciente, out var paciente)
                ? paciente
                : "Sin paciente";
        }

        return View(model);
    }
    // Muestra los detalles de una cita, incluyendo el nombre del servicio asociado
    public async Task<IActionResult> Details(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var cita = await _context.CITAs.FirstOrDefaultAsync(m => m.Id == id);
        if (cita is null)
        {
            return NotFound();
        }

        return View(await BuildDetailsViewModelAsync(cita));
    }

    // Muestra el formulario para crear una nueva cita, prellenando la fecha de la cita para el día siguiente y la fecha de registro con la fecha actual
    public async Task<IActionResult> Create()
    {
        return View(await BuildViewModelAsync(new CITA
        {
            FechaDeLaCita = DateTime.Now.AddDays(1),
            FechaDeRegistro = DateTime.Now
        }));
    }

    // Recibe los datos del formulario para crear una nueva cita, los valida y los guarda en la base de datos
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CitaFormViewModel viewModel)
    {
        if (!await _context.Servicios.AnyAsync())
        {
            ModelState.AddModelError("Cita.IdServicio", "Primero crea al menos un servicio.");
        }

        if (!ModelState.IsValid)
        {
            return View(await BuildViewModelAsync(viewModel.Cita));
        }

        viewModel.Cita.FechaDeRegistro = DateTime.Now;
        _context.Add(viewModel.Cita);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Cita creada con Entity Framework.";
        return RedirectToAction(nameof(Index));
    }
    // Muestra el formulario para editar una cita existente, prellenando los datos actuales de la cita
    public async Task<IActionResult> Edit(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var cita = await _context.CITAs.FindAsync(id);
        if (cita is null)
        {
            return NotFound();
        }

        return View(await BuildViewModelAsync(cita));
    }
    // Recibe los datos del formulario para editar una cita existente, los valida y los actualiza en la base de datos
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CitaFormViewModel viewModel)
    {
        if (id != viewModel.Cita.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(await BuildViewModelAsync(viewModel.Cita));
        }

        var citaDb = await _context.CITAs.FindAsync(id);
        if (citaDb is null)
        {
            return NotFound();
        }


        citaDb.MontoTotal = viewModel.Cita.MontoTotal;
        citaDb.FechaDeLaCita = viewModel.Cita.FechaDeLaCita;
        citaDb.IdServicio = viewModel.Cita.IdServicio;
        citaDb.IdPaciente = viewModel.Cita.IdPaciente;


        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Cita actualizada con Entity Framework.";
        return RedirectToAction(nameof(Index));
    }
    // Muestra el formulario para eliminar una cita existente, mostrando los detalles de la cita y el nombre del servicio asociado
    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var cita = await _context.CITAs.FirstOrDefaultAsync(m => m.Id == id);
        if (cita is null)
        {
            return NotFound();
        }

        return View(await BuildDetailsViewModelAsync(cita));
    }
    // Recibe la confirmación para eliminar una cita existente, la elimina de la base de datos y redirige al índice
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cita = await _context.CITAs.FindAsync(id);
        if (cita is not null)
        {
            _context.CITAs.Remove(cita);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cita eliminada con Entity Framework.";
        }

        return RedirectToAction(nameof(Index));
    }
    // Métodos auxiliares para construir los view models con los datos necesarios para los formularios y detalles
    private async Task<CitaFormViewModel> BuildViewModelAsync(CITA cita)
    {
        return new CitaFormViewModel
        {
            Cita = cita,
            // Lista de Servicios 
            Servicios = await _context.Servicios
                .OrderBy(s => s.Nombre)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = $"{s.Nombre} | Monto: {s.Monto:C}"
                })
                .ToListAsync(),

            // Lista de pacientes
            Pacientes = await _context.Pacientes
                .OrderBy(p => p.NombreDeLaPersona)
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = $"{p.NombreDeLaPersona} | {p.Identificacion}"
                })
                .ToListAsync()
        };
    }
    // Construye el view model para los detalles de una cita, incluyendo el nombre del servicio asociado
    private async Task<CitaDetailsViewModel> BuildDetailsViewModelAsync(CITA cita)
    {

        // Obtener el nombre del servicio asociado a la cita
        var servicio = await _context.Servicios
            .Where(s => s.Id == cita.IdServicio)
            .Select(s => s.Nombre)
            .FirstOrDefaultAsync() ?? "Sin servicio";
        // Obtener el nombre del paciente asociado a la cita
        var paciente = await _context.Pacientes
            .Where(p => p.Id == cita.IdPaciente)
            .Select(p => p.NombreDeLaPersona)
            .FirstOrDefaultAsync() ?? "Sin paciente";

        return new CitaDetailsViewModel
        {
            Cita = cita,
            NombreServicio = servicio,
            NombrePaciente = paciente
        };
    }
}
