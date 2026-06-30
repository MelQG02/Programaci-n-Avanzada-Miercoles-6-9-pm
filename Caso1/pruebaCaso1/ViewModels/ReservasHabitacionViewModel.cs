using pruebaCaso1.Models;

namespace pruebaCaso1.ViewModels
{
    public class ReservasHabitacionViewModel
    {
        public Habitacion Habitacion { get; set; } = null!;

        public List<Reservacion> Reservaciones { get; set; } = new();
    }
}
