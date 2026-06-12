using System;
using System.Collections.Generic;

namespace ClaseEF.Models;

public partial class Servicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public decimal Monto { get; set; }

    public decimal IVA { get; set; }

    public int Especialidad { get; set; }

    public string Especialista { get; set; } = null!;

    public string Clinica { get; set; } = null!;

    public DateTime FechaDeRegistro { get; set; }

    public DateTime? FechaDeModificacion { get; set; }

    public bool ESTADO { get; set; }
}
