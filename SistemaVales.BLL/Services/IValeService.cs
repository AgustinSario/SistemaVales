using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public interface IValeService
{
    Task<IEnumerable<Vale>> ObtenerTodosAsync();
    Task<Vale?> ObtenerPorIdAsync(int id);
    Task CrearValeAsync(Vale vale);
    Task CambiarEstadoAsync(int id, string nuevoEstado, string? entregadoPor = null);
    Task GenerarDesdeExpedienteAsync(int expedienteId, int medicamentoId, decimal monto);
    Task RendirValeAsync(int id);
}
