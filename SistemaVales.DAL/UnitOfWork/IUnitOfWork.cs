using SistemaVales.Models;

namespace SistemaVales.DAL.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    Repositories.IRepository<Hospital> Hospitales { get; }
    Repositories.IRepository<Paciente> Pacientes { get; }
    Repositories.IRepository<Expediente> Expedientes { get; }
    Repositories.IRepository<Medicamento> Medicamentos { get; }
    Repositories.IRepository<Receta> Recetas { get; }
    Repositories.IRepository<Vale> Vales { get; }
    Task<int> CompleteAsync();
}
