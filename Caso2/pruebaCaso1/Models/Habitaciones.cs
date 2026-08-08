using System.ComponentModel.DataAnnotations;

namespace pruebaCaso1.Models
{
    public class Habitacion
    {
        public int Id { get; set; }

        [StringLength(7)]
        public string CodigoDeHabitacion { get; set; } = null!;

        [StringLength(30)]
        public string NombreDeHabitacion { get; set; } = null!;

        public int CantidadDeHuespedesPermitidos { get; set; }
        public int CantidadDeCamas { get; set; }
        public int CantidadDeBanos { get; set; }

        [StringLength(10)]
        public string Ubicacion { get; set; } = null!;

        [StringLength(100)]
        public string EncargadoDeLimpieza { get; set; } = null!;
        public int TipoDeHabitacion { get; set; } 
        public decimal CostoDeLimpieza { get; set; }
        public decimal CostoDeReserva { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }


    }
}
