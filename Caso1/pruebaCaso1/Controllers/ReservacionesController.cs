using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using pruebaCaso1.Data;
using pruebaCaso1.Models;
using pruebaCaso1.ViewModels;

namespace pruebaCaso1.Controllers
{
    public class ReservacionesController : Controller
    {
        private readonly ClaseEfContext _context;

        public ReservacionesController(ClaseEfContext context)
        {
            _context = context;
        }

 
        public async Task<IActionResult> Index()
        {
            var model = new ReservacionIndexViewModel
            {
                Habitaciones = await _context.Habitaciones
                    .Where(h => h.Estado)
                    .ToListAsync(),
                MensajeError = TempData["MensajeError"] as string
            };

            return View(model);
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuscarReserva(int idReservacion)
        {
            var reservacion = await _context.Reservaciones
                .FirstOrDefaultAsync(r => r.Id == idReservacion);

            if (reservacion == null)
            {
                return RedirectToAction(nameof(Index), new
                {
                    mensajeError = "Estimado usuario, no se ha encontrado la reservación, favor realice una."
                });
            }

            return RedirectToAction(nameof(Details), new { id = reservacion.Id });
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return NotFound();

            var reservacion = await _context.Reservaciones
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservacion == null)
                return NotFound();

            var habitacion = await _context.Habitaciones
                .FirstOrDefaultAsync(h => h.Id == reservacion.IdHabitacion);

            var model = new DetallesReservacionViewModel
            {
                Reservacion = reservacion,
                Habitacion = habitacion
            };

            return View(model);
        }


        public async Task<IActionResult> Reserve(int? id)
        {
            if (id is null)
                return NotFound();

            var habitacion = await _context.Habitaciones.FindAsync(id);
            if (habitacion == null)
                return NotFound();

            var model = new ReservarViewModel
            {
                Habitacion = habitacion,
                Reservacion = new Reservacion { IdHabitacion = habitacion.Id }
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReservePost(ReservarViewModel model)
        {

            var habitacion = await _context.Habitaciones
                .FirstOrDefaultAsync(h => h.Id == model.Reservacion.IdHabitacion);

            if (habitacion == null)
                return NotFound();

            model.Habitacion = habitacion;

            foreach (var key in ModelState.Keys.Where(k => k.StartsWith("Habitacion")).ToList())
                ModelState.Remove(key);

            if (model.Reservacion.FechaFinReserva <= model.Reservacion.FechaInicioReserva)
            {
                ModelState.AddModelError(
                    "Reservacion.FechaFinReserva",
                    "La fecha final debe ser mayor que la inicial.");
            }

            if (!ModelState.IsValid)
                return View("Reserve", model);

            int dias = (model.Reservacion.FechaFinReserva -
                        model.Reservacion.FechaInicioReserva).Days;

            model.Reservacion.MontoTotal =
                (dias * habitacion.CostoDeReserva) +
                habitacion.CostoDeLimpieza;

            model.Reservacion.FechaDeRegistro = DateTime.Now;

            _context.Reservaciones.Add(model.Reservacion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = model.Reservacion.Id });
        }
    }
}