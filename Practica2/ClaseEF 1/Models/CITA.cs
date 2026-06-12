using System;
using System.Collections.Generic;

namespace ClaseEF.Models;

public partial class CITA
{
    public int Id { get; set; }

    public decimal MontoTotal { get; set; }

    public DateTime FechaDeLaCita { get; set; }

    public DateTime FechaDeRegistro { get; set; }

    public int IdServicio { get; set; }

    public int IdPaciente { get; set; }
}
