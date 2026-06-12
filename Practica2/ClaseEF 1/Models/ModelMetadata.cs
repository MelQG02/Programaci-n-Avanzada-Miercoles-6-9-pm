using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ClaseEF.Models;

[ModelMetadataType(typeof(CitaMetadata))]
public partial class CITA
{
}

[ModelMetadataType(typeof(ClinicaMetadata))]
public partial class Clinica
{
}

[ModelMetadataType(typeof(EspecialidadMetadata))]
public partial class Especialidade
{
}

[ModelMetadataType(typeof(ServicioMetadata))]
public partial class Servicio
{
}

public class CitaMetadata
{
    [Display(Name = "Persona")]
    [Required]
    [StringLength(150)]
    public string NombreDeLaPersona { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string Identificacion { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string Telefono { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Correo { get; set; } = string.Empty;

    [Display(Name = "Fecha de nacimiento")]
    [DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }

    [Required]
    [StringLength(200)]
    public string Direccion { get; set; } = string.Empty;

    [Display(Name = "Monto total")]
    [Range(0, 99999999)]
    public decimal MontoTotal { get; set; }

    [Display(Name = "Fecha de la cita")]
    [DataType(DataType.DateTime)]
    public DateTime FechaDeLaCita { get; set; }

    [Display(Name = "Fecha de registro")]
    [DataType(DataType.DateTime)]
    public DateTime FechaDeRegistro { get; set; }

    [Display(Name = "Servicio")]
    public int IdServicio { get; set; }
}

public class ClinicaMetadata
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(200)]
    public string? Direccion { get; set; }
}

public class EspecialidadMetadata
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
}

public class ServicioMetadata
{
    [Required]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Descripcion { get; set; } = string.Empty;

    [Range(0, 99999999)]
    public decimal Monto { get; set; }

    [Range(0, 99999999)]
    public decimal IVA { get; set; }

    [Display(Name = "Especialidad")]
    public int Especialidad { get; set; }

    [Required]
    [StringLength(200)]
    public string Especialista { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Clinica { get; set; } = string.Empty;

    [Display(Name = "Fecha de registro")]
    [DataType(DataType.DateTime)]
    public DateTime FechaDeRegistro { get; set; }

    [Display(Name = "Fecha de modificacion")]
    [DataType(DataType.DateTime)]
    public DateTime? FechaDeModificacion { get; set; }

    [Display(Name = "Activo")]
    public bool ESTADO { get; set; }
}
