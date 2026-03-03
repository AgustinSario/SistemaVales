using SistemaVales.DAL.UnitOfWork;
using SistemaVales.Models;
using Microsoft.EntityFrameworkCore;

namespace SistemaVales.BLL.Services;

public class ValeService : IValeService
{
    private readonly IUnitOfWork _unitOfWork;

    public ValeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Vale>> ObtenerTodosAsync()
    {
        return await _unitOfWork.Vales.GetAllAsync();
    }

    public async Task<Vale?> ObtenerPorIdAsync(int id)
    {
        var vale = await _unitOfWork.Vales.GetByIdAsync(id);
        if (vale != null)
        {
            // Cargar relaciones si es necesario (el repositorio genérico podría no hacerlo)
            // Aquí asumo que el repositorio tiene una forma de incluir. Si no, lo dejamos así por ahora.
        }
        return vale;
    }

    public async Task CrearValeAsync(Vale vale)
    {
        // Generar número de vale automático (ej: V-2026-0001)
        var count = (await _unitOfWork.Vales.GetAllAsync()).Count();
        vale.NumeroVale = $"V-{DateTime.Now.Year}-{(count + 1).ToString("D4")}";
        
        vale.FechaEmision = DateTime.Now;
        vale.Estado = "Iniciado";

        await _unitOfWork.Vales.AddAsync(vale);
        await _unitOfWork.CompleteAsync();
    }

    public async Task CambiarEstadoAsync(int id, string nuevoEstado, string? entregadoPor = null)
    {
        var vale = await _unitOfWork.Vales.GetByIdAsync(id);
        if (vale != null)
        {
            vale.Estado = nuevoEstado;
            if (!string.IsNullOrEmpty(entregadoPor))
            {
                vale.EntregadoPor = entregadoPor;
            }
            _unitOfWork.Vales.Update(vale);
            await _unitOfWork.CompleteAsync();
        }
    }

    public async Task GenerarDesdeExpedienteAsync(int expedienteId, int medicamentoId, decimal monto)
    {
        var expediente = await _unitOfWork.Expedientes.GetByIdAsync(expedienteId);
        if (expediente == null) throw new Exception("Expediente no encontrado");

        var vale = new Vale
        {
            ExpedienteId = expedienteId,
            MedicamentoId = medicamentoId,
            Monto = monto,
            FechaEmision = DateTime.Now,
            Estado = "Iniciado"
        };

        // Generar número de vale
        var count = (await _unitOfWork.Vales.GetAllAsync()).Count();
        vale.NumeroVale = $"V-{DateTime.Now.Year}-{(count + 1).ToString("D4")}";

        await _unitOfWork.Vales.AddAsync(vale);
        
        // Actualizar estado del expediente
        expediente.Estado = "ValeGenerado";
        _unitOfWork.Expedientes.Update(expediente);

        await _unitOfWork.CompleteAsync();
    }

    public async Task RendirValeAsync(int id)
    {
        await CambiarEstadoAsync(id, "Entregado"); // En este contexto, Rendir se mapea a Entregado según el nuevo flujo
    }
}
