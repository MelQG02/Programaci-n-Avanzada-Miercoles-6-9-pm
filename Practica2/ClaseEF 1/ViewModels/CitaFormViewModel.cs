using ClaseEF.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClaseEF.ViewModels;

public class CitaFormViewModel
{
    public CITA Cita { get; set; } = new();

    public List<SelectListItem> Servicios { get; set; } = [];

    // Agrega una propiedad para la lista de pacientes, que se llenará con los datos de la base de datos en el controlador
    public List<SelectListItem> Pacientes { get; set; } = [];
}
