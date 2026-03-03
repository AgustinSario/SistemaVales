using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public interface IExpedienteService
{
    Task<IEnumerable<Expediente>> ObtenerTodosAsync();
    Task<Expediente?> ObtenerPorIdAsync(int id);
    Task CrearExpedienteAsync(Expediente expediente);
    Task IniciarDesdeRecetaAsync(int recetaId, string numeroExpediente);
    Task ProcesarInformeTecnicoAsync(int expedienteId, bool cumpleFormulario, bool? informeOT, string observaciones);
    Task ProcesarAuditoriaMedicaAsync(int expedienteId, string resultado);
    Task ActualizarEstadoAsync(int id, string nuevoEstado);
}
