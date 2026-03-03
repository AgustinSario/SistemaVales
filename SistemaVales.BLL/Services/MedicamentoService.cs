using SistemaVales.DAL.UnitOfWork;
using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public class MedicamentoService : IMedicamentoService
{
    private readonly IUnitOfWork _unitOfWork;

    public MedicamentoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Medicamento>> ObtenerTodosAsync()
    {
        return await _unitOfWork.Medicamentos.GetAllAsync();
    }

    public async Task<Medicamento?> ObtenerPorIdAsync(int id)
    {
        return await _unitOfWork.Medicamentos.GetByIdAsync(id);
    }

    public async Task CrearMedicamentoAsync(Medicamento medicamento)
    {
        await _unitOfWork.Medicamentos.AddAsync(medicamento);
        await _unitOfWork.CompleteAsync();
    }

    public async Task ActualizarMedicamentoAsync(Medicamento medicamento)
    {
        _unitOfWork.Medicamentos.Update(medicamento);
        await _unitOfWork.CompleteAsync();
    }

    public async Task EliminarMedicamentoAsync(int id)
    {
        var medicamento = await _unitOfWork.Medicamentos.GetByIdAsync(id);
        if (medicamento != null)
        {
            _unitOfWork.Medicamentos.Remove(medicamento);
            await _unitOfWork.CompleteAsync();
        }
    }
}
