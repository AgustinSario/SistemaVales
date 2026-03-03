using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVales.BLL.Services;
using SistemaVales.Models;

namespace SistemaVales.Web.Controllers;

[Authorize]
public class RecetasController : Controller
{
    private readonly IRecetaService _recetaService;
    private readonly IPacienteService _pacienteService;
    private readonly IHospitalService _hospitalService;
    private readonly IWebHostEnvironment _hostEnvironment;

    public RecetasController(
        IRecetaService recetaService,
        IPacienteService pacienteService,
        IHospitalService hospitalService,
        IWebHostEnvironment hostEnvironment)
    {
        _recetaService = recetaService;
        _pacienteService = pacienteService;
        _hospitalService = hospitalService;
        _hostEnvironment = hostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var recetas = await _recetaService.ObtenerTodasAsync();
        return View(recetas);
    }

    public async Task<IActionResult> Create()
    {
        var pacientes = await _pacienteService.ObtenerTodosAsync();
        var hospitales = await _hospitalService.ObtenerTodosAsync();
        ViewBag.PacienteId = new SelectList(pacientes, "Id", "Nombre");
        ViewBag.HospitalId = new SelectList(hospitales, "Id", "Nombre");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("DNI,InformeSocialEconomico,TieneObraSocial,AnalisisClinicos,MontoTotal,EsAsistenciaComunidad,PacienteId,HospitalId")] Receta receta, IFormFile? imagenFile)
    {
        if (ModelState.IsValid)
        {
            if (imagenFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagenFile.FileName);
                string path = Path.Combine(wwwRootPath, "uploads", "recetas");
                
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await imagenFile.CopyToAsync(fileStream);
                }
                receta.ImagenUrl = "/uploads/recetas/" + fileName;
            }

            await _recetaService.CrearRecetaAsync(receta);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.PacienteId = new SelectList(await _pacienteService.ObtenerTodosAsync(), "Id", "Nombre", receta.PacienteId);
        ViewBag.HospitalId = new SelectList(await _hospitalService.ObtenerTodosAsync(), "Id", "Nombre", receta.HospitalId);
        return View(receta);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var receta = await _recetaService.ObtenerPorIdAsync(id);
        if (receta == null) return NotFound();

        ViewBag.PacienteId = new SelectList(await _pacienteService.ObtenerTodosAsync(), "Id", "Nombre", receta.PacienteId);
        ViewBag.HospitalId = new SelectList(await _hospitalService.ObtenerTodosAsync(), "Id", "Nombre", receta.HospitalId);
        return View(receta);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,DNI,InformeSocialEconomico,TieneObraSocial,AnalisisClinicos,MontoTotal,EsAsistenciaComunidad,ImagenUrl,FechaCreacion,PacienteId,HospitalId")] Receta receta, IFormFile? imagenFile)
    {
        if (id != receta.Id) return NotFound();

        if (ModelState.IsValid)
        {
            if (imagenFile != null)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(imagenFile.FileName);
                string path = Path.Combine(wwwRootPath, "uploads", "recetas");

                using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    await imagenFile.CopyToAsync(fileStream);
                }
                receta.ImagenUrl = "/uploads/recetas/" + fileName;
            }

            await _recetaService.ActualizarRecetaAsync(receta);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.PacienteId = new SelectList(await _pacienteService.ObtenerTodosAsync(), "Id", "Nombre", receta.PacienteId);
        ViewBag.HospitalId = new SelectList(await _hospitalService.ObtenerTodosAsync(), "Id", "Nombre", receta.HospitalId);
        return View(receta);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var receta = await _recetaService.ObtenerPorIdAsync(id);
        if (receta == null) return NotFound();
        return View(receta);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _recetaService.EliminarRecetaAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
