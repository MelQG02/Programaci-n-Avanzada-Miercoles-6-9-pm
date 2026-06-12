using System.Diagnostics;
using ClaseEF.Data;
using ClaseEF.Models;
using ClaseEF.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClaseEF.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ClaseEfContext _context;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, ClaseEfContext context, IConfiguration configuration)
    {
        _logger = logger;
        _context = context;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeDashboardViewModel
        {
            TotalCitas = await _context.CITAs.CountAsync(),
            TotalClinicas = await _context.Clinicas.CountAsync(),
            TotalEspecialidades = await _context.Especialidades.CountAsync(),
            TotalServicios = await _context.Servicios.CountAsync(),
            ConnectionString = _configuration.GetConnectionString("ClaseEFDb") ?? string.Empty
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
