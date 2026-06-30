using Microsoft.AspNetCore.Mvc.Rendering;
using pruebaCaso1.Models;

namespace pruebaCaso1.ViewModels
{
    public class ReservacionIndexViewModel
    {
        public List<Habitacion> Habitaciones { get; set; } = [];
        public string? MensajeError { get; set; }
    }
}
