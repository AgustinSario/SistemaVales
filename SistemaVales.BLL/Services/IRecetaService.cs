using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public interface IRecetaService
{
    Task<IEnumerable<Receta>> ObtenerTodasAsync();
    Task<Receta?> ObtenerPorIdAsync(int id);
    Task CrearRecetaAsync(Receta receta);
    Task ActualizarRecetaAsync(Receta receta);
    Task EliminarRecetaAsync(int id);
}
