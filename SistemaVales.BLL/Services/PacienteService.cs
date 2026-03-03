using SistemaVales.DAL.UnitOfWork;
using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public class PacienteService : IPacienteService
{
    private readonly IUnitOfWork _unitOfWork;

    public PacienteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Paciente>> ObtenerTodosAsync()
    {
        return await _unitOfWork.Pacientes.GetAllAsync();
    }

    public async Task<Paciente?> ObtenerPorIdAsync(int id)
    {
        return await _unitOfWork.Pacientes.GetByIdAsync(id);
    }

    public async Task CrearPacienteAsync(Paciente paciente)
    {
        await _unitOfWork.Pacientes.AddAsync(paciente);
        await _unitOfWork.CompleteAsync();
    }

    public async Task ActualizarPacienteAsync(Paciente paciente)
    {
        _unitOfWork.Pacientes.Update(paciente);
        await _unitOfWork.CompleteAsync();
    }

    public async Task EliminarPacienteAsync(int id)
    {
        var paciente = await _unitOfWork.Pacientes.GetByIdAsync(id);
        if (paciente != null)
        {
            _unitOfWork.Pacientes.Remove(paciente);
            await _unitOfWork.CompleteAsync();
        }
    }
}
