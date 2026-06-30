using pruebaCaso1.Models;

namespace pruebaCaso1.ViewModels
{
    public class DetallesReservacionViewModel
    {
        public Reservacion Reservacion { get; set; } = new ();
        public Habitacion? Habitacion { get; set; }

        public int CantidadDias => (int)(Reservacion.FechaFinReserva - Reservacion.FechaInicioReserva).TotalDays;
    }
}
