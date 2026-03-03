using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVales.BLL.Services;
using SistemaVales.Models;

namespace SistemaVales.Web.Controllers;

[Authorize]
public class FarmaciaController : Controller
{
    private readonly IExpedienteService _expedienteService;

    public FarmaciaController(IExpedienteService expedienteService)
    {
        _expedienteService = expedienteService;
    }

    public async Task<IActionResult> Index()
    {
        var expedientes = await _expedienteService.ObtenerTodosAsync();
        // Filtrar por estados que incumben a farmacia
        var pendientes = expedientes.Where(e => e.Estado == "PendienteInformeTecnico" || e.Estado == "Desfavorable");
        return View(pendientes);
    }

    public async Task<IActionResult> Informe(int id)
    {
        var expediente = await _expedienteService.ObtenerPorIdAsync(id);
        if (expediente == null) return NotFound();

        return View(expediente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcesarInforme(int id, bool cumpleFormulario, bool? informeOT, string observaciones)
    {
        await _expedienteService.ProcesarInformeTecnicoAsync(id, cumpleFormulario, informeOT, observaciones);
        TempData["Success"] = "Informe técnico procesado correctamente.";
        return RedirectToAction(nameof(Index));
    }
}
