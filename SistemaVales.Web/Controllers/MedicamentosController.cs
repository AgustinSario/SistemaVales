using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVales.BLL.Services;
using SistemaVales.Models;

namespace SistemaVales.Web.Controllers;

[Authorize]
public class MedicamentosController : Controller
{
    private readonly IMedicamentoService _medicamentoService;

    public MedicamentosController(IMedicamentoService medicamentoService)
    {
        _medicamentoService = medicamentoService;
    }

    public async Task<IActionResult> Index()
    {
        var medicamentos = await _medicamentoService.ObtenerTodosAsync();
        return View(medicamentos);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Cantidad,Unidad,Descripcion,MontoEstimado")] Medicamento medicamento)
    {
        if (ModelState.IsValid)
        {
            await _medicamentoService.CrearMedicamentoAsync(medicamento);
            return RedirectToAction(nameof(Index));
        }
        return View(medicamento);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var medicamento = await _medicamentoService.ObtenerPorIdAsync(id);
        if (medicamento == null) return NotFound();

        return View(medicamento);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Cantidad,Unidad,Descripcion,MontoEstimado")] Medicamento medicamento)
    {
        if (id != medicamento.Id) return NotFound();

        if (ModelState.IsValid)
        {
            await _medicamentoService.ActualizarMedicamentoAsync(medicamento);
            return RedirectToAction(nameof(Index));
        }
        return View(medicamento);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var medicamento = await _medicamentoService.ObtenerPorIdAsync(id);
        if (medicamento == null) return NotFound();
        return View(medicamento);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _medicamentoService.EliminarMedicamentoAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
