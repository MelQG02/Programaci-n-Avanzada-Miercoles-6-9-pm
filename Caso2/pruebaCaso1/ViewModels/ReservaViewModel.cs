using pruebaCaso1.Models;

namespace pruebaCaso1.ViewModels
{
    public class ReservarViewModel
    {
        public Reservacion Reservacion { get; set; } = new ();
        public Habitacion Habitacion { get; set; } = new ();
    }
}
