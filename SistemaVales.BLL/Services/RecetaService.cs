using SistemaVales.DAL.UnitOfWork;
using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public class RecetaService : IRecetaService
{
    private readonly IUnitOfWork _unitOfWork;

    public RecetaService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Receta>> ObtenerTodasAsync()
    {
        return await _unitOfWork.Recetas.GetAllAsync();
    }

    public async Task<Receta?> ObtenerPorIdAsync(int id)
    {
        return await _unitOfWork.Recetas.GetByIdAsync(id);
    }

    public async Task CrearRecetaAsync(Receta receta)
    {
        await _unitOfWork.Recetas.AddAsync(receta);
        await _unitOfWork.CompleteAsync();
    }

    public async Task ActualizarRecetaAsync(Receta receta)
    {
        _unitOfWork.Recetas.Update(receta);
        await _unitOfWork.CompleteAsync();
    }

    public async Task EliminarRecetaAsync(int id)
    {
        var receta = await _unitOfWork.Recetas.GetByIdAsync(id);
        if (receta != null)
        {
            _unitOfWork.Recetas.Remove(receta);
            await _unitOfWork.CompleteAsync();
        }
    }
}
