using ClaseEF.Models;

namespace ClaseEF.ViewModels;

public class CitaDetailsViewModel
{
    public CITA Cita { get; set; } = new();

    public string NombreServicio { get; set; } = "Sin servicio";

    public string NombrePaciente { get; set; } = "Sin paciente";
}
