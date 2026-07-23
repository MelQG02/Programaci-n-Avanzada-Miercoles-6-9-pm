namespace ClaseEF.Models
{
    public class Paciente
    {
        public int Id { get; set; }

        public string NombreDeLaPersona { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; } 
        public string Direccion { get; set; } = null!;
        public string? UserId { get; set; }
    }
}
