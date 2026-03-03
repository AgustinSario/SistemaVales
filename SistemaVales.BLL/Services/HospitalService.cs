using SistemaVales.DAL.UnitOfWork;
using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public class HospitalService : IHospitalService
{
    private readonly IUnitOfWork _unitOfWork;

    public HospitalService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Hospital>> ObtenerTodosAsync()
    {
        return await _unitOfWork.Hospitales.GetAllAsync();
    }

    public async Task<Hospital?> ObtenerPorIdAsync(int id)
    {
        return await _unitOfWork.Hospitales.GetByIdAsync(id);
    }

    public async Task CrearHospitalAsync(Hospital hospital)
    {
        await _unitOfWork.Hospitales.AddAsync(hospital);
        await _unitOfWork.CompleteAsync();
    }

    public async Task ActualizarHospitalAsync(Hospital hospital)
    {
        _unitOfWork.Hospitales.Update(hospital);
        await _unitOfWork.CompleteAsync();
    }

    public async Task EliminarHospitalAsync(int id)
    {
        var hospital = await _unitOfWork.Hospitales.GetByIdAsync(id);
        if (hospital != null)
        {
            _unitOfWork.Hospitales.Remove(hospital);
            await _unitOfWork.CompleteAsync();
        }
    }
}
