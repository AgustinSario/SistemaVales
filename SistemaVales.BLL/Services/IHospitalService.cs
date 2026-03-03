using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public interface IHospitalService
{
    Task<IEnumerable<Hospital>> ObtenerTodosAsync();
    Task<Hospital?> ObtenerPorIdAsync(int id);
    Task CrearHospitalAsync(Hospital hospital);
    Task ActualizarHospitalAsync(Hospital hospital);
    Task EliminarHospitalAsync(int id);
}
