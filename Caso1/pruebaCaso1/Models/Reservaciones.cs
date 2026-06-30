using System.ComponentModel.DataAnnotations;

namespace pruebaCaso1.Models
{
    public class Reservacion
    {
        public int Id { get; set; }

        [StringLength(150)]
        public string NombreDeLaPersona { get; set; } = null!;
        [StringLength(30)]
        public string Identificacion { get; set; } = null!;

        [StringLength(10)]
        public string Telefono { get; set; } = null!;
        [StringLength(50)]
        public string Correo { get; set; } = null!;

        public DateTime FechaNacimiento { get; set; }

        [StringLength(200)]
        public string Direccion { get; set; } = null!;
        public decimal MontoTotal { get; set; }
        public DateTime FechaInicioReserva { get; set; }
        public DateTime FechaFinReserva { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public int IdHabitacion { get; set; }

    }
}
