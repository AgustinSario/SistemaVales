using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SistemaVales.BLL.Services;
using SistemaVales.Models;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace SistemaVales.Web.Controllers;

[Authorize]
public class ValesController : Controller
{
    private readonly IValeService _valesService;
    private readonly IExpedienteService _expedienteService;
    private readonly IMedicamentoService _medicamentoService;

    public ValesController(IValeService valesService, IExpedienteService expediteService, IMedicamentoService medicamentoService)
    {
        _valesService = valesService;
        _expedienteService = expediteService;
        _medicamentoService = medicamentoService;
    }

    public async Task<IActionResult> Index()
    {
        var vales = await _valesService.ObtenerTodosAsync();
        return View(vales);
    }

    public async Task<IActionResult> Details(int id)
    {
        var vale = await _valesService.ObtenerPorIdAsync(id);
        if (vale == null) return NotFound();

        // Cargar expediente completo para datos del QR
        var expediente = await _expedienteService.ObtenerPorIdAsync(vale.ExpedienteId);
        vale.Expediente = expediente;

        // Generar QR
        string qrData = $"Vale: {vale.NumeroVale}\nDNI: {expediente?.Paciente?.DNI}\nConcepto: {vale.Medicamento?.Nombre}\nMonto: ${vale.Monto}";
        
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q))
        using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
        {
            byte[] qrCodeImage = qrCode.GetGraphic(20);
            ViewBag.QRCodeImage = Convert.ToBase64String(qrCodeImage);
        }

        return View(vale);
    }

    public async Task<IActionResult> GenerarDesdeExpediente(int id)
    {
        var expediente = await _expedienteService.ObtenerPorIdAsync(id);
        if (expediente == null) return NotFound();

        ViewBag.Expediente = expediente;
        
        // Cargar todos los medicamentos para el dropdown global
        var medicamentos = await _medicamentoService.ObtenerTodosAsync();
        ViewBag.Medicamentos = new SelectList(medicamentos, "Id", "Nombre");
        ViewBag.MedicamentosOriginal = medicamentos;

        var vale = new Vale
        {
            ExpedienteId = id,
            Monto = 0 // Inicialmente 0, se llenará vía JS
        };

        return View(vale);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> GenerarDesdeExpediente(int expedienteId, int medicamentoId, decimal monto)
    {
        await _valesService.GenerarDesdeExpedienteAsync(expedienteId, medicamentoId, monto);
        return RedirectToAction("Index", "Expedientes");
    }
}
