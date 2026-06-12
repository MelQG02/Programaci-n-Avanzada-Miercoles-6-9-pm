namespace ClaseEF.ViewModels;

public class HomeDashboardViewModel
{
    public int TotalClinicas { get; set; }

    public int TotalEspecialidades { get; set; }

    public int TotalServicios { get; set; }

    public int TotalCitas { get; set; }

    public string ConnectionString { get; set; } = string.Empty;
}
