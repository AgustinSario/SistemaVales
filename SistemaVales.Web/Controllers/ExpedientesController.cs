using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVales.BLL.Services;
using SistemaVales.Models;

namespace SistemaVales.Web.Controllers;

[Authorize]
public class ExpedientesController : Controller
{
    private readonly IExpedienteService _expedientesService;
    private readonly IPacienteService _pacienteService;
    private readonly IRecetaService _recetaService;

    public ExpedientesController(IExpedienteService expedientesService, IPacienteService pacienteService, IRecetaService recetaService)
    {
        _expedientesService = expedientesService;
        _pacienteService = pacienteService;
        _recetaService = recetaService;
    }

    public async Task<IActionResult> Index()
    {
        var expedientes = await _expedientesService.ObtenerTodosAsync();
        return View(expedientes);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var expediente = await _expedientesService.ObtenerPorIdAsync(id.Value);
        if (expediente == null) return NotFound();

        return View(expediente);
    }

    public async Task<IActionResult> IniciarDesdeReceta(int recetaId)
    {
        var receta = await _recetaService.ObtenerPorIdAsync(recetaId);
        if (receta == null) return NotFound();

        ViewBag.Receta = receta;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> IniciarDesdeReceta(int recetaId, string numeroExpediente)
    {
        if (string.IsNullOrEmpty(numeroExpediente))
        {
            ModelState.AddModelError("", "El número de expediente es obligatorio");
            ViewBag.Receta = await _recetaService.ObtenerPorIdAsync(recetaId);
            return View();
        }

        await _expedientesService.IniciarDesdeRecetaAsync(recetaId, numeroExpediente);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Workflow(int id)
    {
        var expediente = await _expedientesService.ObtenerPorIdAsync(id);
        if (expediente == null) return NotFound();

        // Cargar receta si existe para mostrar info
        if (expediente.RecetaId.HasValue)
        {
            expediente.Receta = await _recetaService.ObtenerPorIdAsync(expediente.RecetaId.Value);
        }

        return View(expediente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcesarInformeTecnico(int id, bool cumpleFormulario, bool? informeOT, string observaciones)
    {
        await _expedientesService.ProcesarInformeTecnicoAsync(id, cumpleFormulario, informeOT, observaciones);
        return RedirectToAction(nameof(Workflow), new { id = id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProcesarAuditoriaMedica(int id, string resultado)
    {
        await _expedientesService.ProcesarAuditoriaMedicaAsync(id, resultado);
        return RedirectToAction(nameof(Workflow), new { id = id });
    }

    public async Task<IActionResult> Create()
    {
        var pacientes = await _pacienteService.ObtenerTodosAsync();
        ViewBag.Pacientes = new SelectList(pacientes, "Id", "Nombre");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("NumeroExpediente,PacienteId,InformeSocialEconomico,HistoriaClinica,Receta")] Expediente expediente)
    {
        if (ModelState.IsValid)
        {
            await _expedientesService.CrearExpedienteAsync(expediente);
            return RedirectToAction(nameof(Index));
        }
        
        var pacientes = await _pacienteService.ObtenerTodosAsync();
        ViewBag.Pacientes = new SelectList(pacientes, "Id", "Nombre", expediente.PacienteId);
        return View(expediente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Aprobar(int id)
    {
        await _expedientesService.ActualizarEstadoAsync(id, "Favorable");
        return RedirectToAction(nameof(Index));
    }
}
