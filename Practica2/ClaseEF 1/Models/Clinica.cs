using System;
using System.Collections.Generic;

namespace ClaseEF.Models;

public partial class Clinica
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }
}
