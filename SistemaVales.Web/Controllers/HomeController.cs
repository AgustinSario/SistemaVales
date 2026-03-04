using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SistemaVales.Web.Models;

namespace SistemaVales.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly SistemaVales.DAL.ValesDbContext _context;

    public HomeController(ILogger<HomeController> logger, SistemaVales.DAL.ValesDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var expedientesCreados = _context.Expedientes.Count();
        var expedientesEnCompras = _context.Expedientes.Count(e => e.Estado == "EnCompras");
        
        var valesCreados = _context.Vales.Count();
        var valesEntregados = _context.Vales.Count(v => v.Estado == "Entregado");
        
        var recetasCreadas = _context.Recetas.Count();
        
        // Asumimos que "evaluadas" son aquellas atadas a un expediente que ya no está "Iniciado" ni "Pendiente"
        var recetasEvaluadas = _context.Expedientes
            .Where(e => e.RecetaId != null && e.Estado != "Iniciado" && e.Estado != "PendienteInformeTecnico")
            .Count();

        var dashboardStats = new DashboardViewModel
        {
            ExpedientesCreados = expedientesCreados,
            ExpedientesEnCompras = expedientesEnCompras,
            ValesCreados = valesCreados,
            ValesEntregados = valesEntregados,
            RecetasCreadas = recetasCreadas,
            RecetasEvaluadas = recetasEvaluadas
        };

        return View(dashboardStats);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
