using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public interface IPacienteService
{
    Task<IEnumerable<Paciente>> ObtenerTodosAsync();
    Task<Paciente?> ObtenerPorIdAsync(int id);
    Task CrearPacienteAsync(Paciente paciente);
    Task ActualizarPacienteAsync(Paciente paciente);
    Task EliminarPacienteAsync(int id);
}
