using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVales.BLL.Services;
using SistemaVales.Models;

namespace SistemaVales.Web.Controllers;

[Authorize]
public class PacientesController : Controller
{
    private readonly IPacienteService _pacienteService;
    private readonly IHospitalService _hospitalService;

    public PacientesController(IPacienteService pacienteService, IHospitalService hospitalService)
    {
        _pacienteService = pacienteService;
        _hospitalService = hospitalService;
    }

    private async Task CargarHospitalesViewBag(int? selectedId = null)
    {
        var hospitales = await _hospitalService.ObtenerTodosAsync();
        ViewBag.Hospitales = new SelectList(hospitales, "Id", "Nombre", selectedId);
    }

    // GET: Pacientes
    public async Task<IActionResult> Index()
    {
        var pacientes = await _pacienteService.ObtenerTodosAsync();
        return View(pacientes);
    }

    // GET: Pacientes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var paciente = await _pacienteService.ObtenerPorIdAsync(id.Value);
        if (paciente == null) return NotFound();

        return View(paciente);
    }

    // GET: Pacientes/Create
    public async Task<IActionResult> Create()
    {
        await CargarHospitalesViewBag();
        return View();
    }

    // POST: Pacientes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,DNI,Direccion,Telefono,HospitalId")] Paciente paciente)
    {
        if (ModelState.IsValid)
        {
            await _pacienteService.CrearPacienteAsync(paciente);
            return RedirectToAction(nameof(Index));
        }
        await CargarHospitalesViewBag(paciente.HospitalId);
        return View(paciente);
    }

    // GET: Pacientes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var paciente = await _pacienteService.ObtenerPorIdAsync(id.Value);
        if (paciente == null) return NotFound();
        await CargarHospitalesViewBag(paciente.HospitalId);
        return View(paciente);
    }

    // POST: Pacientes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,DNI,Direccion,Telefono,HospitalId")] Paciente paciente)
    {
        if (id != paciente.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _pacienteService.ActualizarPacienteAsync(paciente);
            return RedirectToAction(nameof(Index));
        }
        await CargarHospitalesViewBag(paciente.HospitalId);
        return View(paciente);
    }

    // GET: Pacientes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var paciente = await _pacienteService.ObtenerPorIdAsync(id.Value);
        if (paciente == null) return NotFound();

        return View(paciente);
    }

    // POST: Pacientes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _pacienteService.EliminarPacienteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
