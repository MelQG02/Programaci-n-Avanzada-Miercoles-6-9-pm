using Microsoft.AspNetCore.Mvc;
using pruebaCaso1.Data;
using pruebaCaso1.Models;
using pruebaCaso1.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace pruebaCaso1.Controllers
{
    public class HabitacionesController : Controller
    {
        private readonly ClaseEfContext _context;
        public HabitacionesController(ClaseEfContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Habitaciones.ToListAsync());
        }

        public IActionResult Create()
        {
            return View(new Habitacion());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Habitacion habitacion)
        {
            if (!ModelState.IsValid)
            {
                return View(habitacion);
            }
            habitacion.FechaDeRegistro = DateTime.Now;
            _context.Add(habitacion);
            await _context.SaveChangesAsync();
            TempData["SuccessMessageHabitacion"] = "Habitación creada correctamente.";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion is null)
            {
                return NotFound();
            }

            return View(habitacion);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Habitacion habitacion)
        {
            if (id != habitacion.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(habitacion);

            }
            try
            {
                habitacion.FechaDeModificacion = DateTime.Now;
                _context.Update(habitacion);
                await _context.SaveChangesAsync();
                TempData["SuccessMessageHabitacion"] = "Habitación actualizada correctamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Habitaciones.AnyAsync(e => e.Id == habitacion.Id))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FilterList(int id)
        {
            var habitacion = await _context.Habitaciones
                .FirstOrDefaultAsync(h => h.Id == id);

            if (habitacion == null)
            {
                return NotFound();
            }

            var reservaciones = await _context.Reservaciones
                .Where(r => r.IdHabitacion == id)
                .ToListAsync();

            var model = new ReservasHabitacionViewModel
            {
                Habitacion = habitacion,
                Reservaciones = reservaciones
            };

            return View(model);
        }


    }
}