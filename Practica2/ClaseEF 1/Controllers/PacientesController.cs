using ClaseEF.Data;
using ClaseEF.Models;
using ClaseEF.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ClaseEF.Controllers
{
    public class PacientesController : Controller
    {
        private readonly ClaseEfContext _context;
        //Traigo el contexto de la base de datos 
        public PacientesController(ClaseEfContext context)
        {
            _context = context;
        }

        //Lista los pacientes ordenados por nombre de la persona
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pacientes.OrderBy(c => c.NombreDeLaPersona).ToListAsync());
        }

        //Muestra los detalles de un paciente específico
        public async Task<IActionResult> Details(int? id)
        {
            //Si el id es nulo, devuelve un error 404
            if (id is null)
            {
                return NotFound();
            }

            //Busca el paciente con el id especificado en la base de datos
            //y lo devuelve a la vista. Si no lo encuentra, devuelve un error 404
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(m => m.Id == id);
            if (paciente is null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        //Muestra el formulario para crear un nuevo paciente
        public IActionResult Create()
        {
            return View(new Paciente
            {
                FechaNacimiento = DateTime.Today.AddYears(-18)
            });
        }

        //Recibe los datos del formulario para crear un nuevo paciente,
        //los valida y los guarda en la base de datos
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (!ModelState.IsValid)
            {
                return View(paciente);
            }

            _context.Add(paciente);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Paciente creada con Entity Framework.";
            return RedirectToAction(nameof(Index));
        }

        //Muestra el formulario para editar un paciente existente
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente is null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        //Recibe los datos del formulario para editar un paciente existente,
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(paciente);

            }
            try
            {
                _context.Update(paciente);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "paciente actualizada con Entity Framework.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Pacientes.AnyAsync(e => e.Id == paciente.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        //Muestra el formulario para eliminar un paciente existente
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var paciente = await _context.Pacientes.FirstOrDefaultAsync(m => m.Id == id);
            if (paciente is null)
            {
                return NotFound();
            }

            return View(paciente);
        }
        //Recibe la confirmación para eliminar un paciente existente,
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente is not null)
            {
                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "paciente eliminada con Entity Framework.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
