using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public interface IMedicamentoService
{
    Task<IEnumerable<Medicamento>> ObtenerTodosAsync();
    Task<Medicamento?> ObtenerPorIdAsync(int id);
    Task CrearMedicamentoAsync(Medicamento medicamento);
    Task ActualizarMedicamentoAsync(Medicamento medicamento);
    Task EliminarMedicamentoAsync(int id);
}
