using ClaseEF.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClaseEF.ViewModels;

public class ServicioFormViewModel
{
    public Servicio Servicio { get; set; } = new();

    public List<SelectListItem> Especialidades { get; set; } = [];

    public List<string> ClinicasExistentes { get; set; } = [];
}
