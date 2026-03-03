using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaVales.BLL.Services;
using SistemaVales.Models;

namespace SistemaVales.Web.Controllers;

[Authorize]
public class HospitalesController : Controller
{
    private readonly IHospitalService _hospitalService;

    public HospitalesController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    // GET: Hospitales
    public async Task<IActionResult> Index()
    {
        var hospitales = await _hospitalService.ObtenerTodosAsync();
        return View(hospitales);
    }

    // GET: Hospitales/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var hospital = await _hospitalService.ObtenerPorIdAsync(id.Value);
        if (hospital == null) return NotFound();

        return View(hospital);
    }

    // GET: Hospitales/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Hospitales/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Direccion,Telefono,Ciudad")] Hospital hospital)
    {
        if (ModelState.IsValid)
        {
            await _hospitalService.CrearHospitalAsync(hospital);
            return RedirectToAction(nameof(Index));
        }
        return View(hospital);
    }

    // GET: Hospitales/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var hospital = await _hospitalService.ObtenerPorIdAsync(id.Value);
        if (hospital == null) return NotFound();
        return View(hospital);
    }

    // POST: Hospitales/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,Telefono,Ciudad")] Hospital hospital)
    {
        if (id != hospital.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _hospitalService.ActualizarHospitalAsync(hospital);
            return RedirectToAction(nameof(Index));
        }
        return View(hospital);
    }

    // GET: Hospitales/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var hospital = await _hospitalService.ObtenerPorIdAsync(id.Value);
        if (hospital == null) return NotFound();

        return View(hospital);
    }

    // POST: Hospitales/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _hospitalService.EliminarHospitalAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
