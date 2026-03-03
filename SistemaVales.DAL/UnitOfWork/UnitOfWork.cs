using SistemaVales.DAL.Repositories;
using SistemaVales.Models;

namespace SistemaVales.DAL.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ValesDbContext _context;

    public UnitOfWork(ValesDbContext context)
    {
        _context = context;
        Hospitales = new Repository<Hospital>(_context);
        Pacientes = new Repository<Paciente>(_context);
        Expedientes = new Repository<Expediente>(_context);
        Medicamentos = new Repository<Medicamento>(_context);
        Recetas = new Repository<Receta>(_context);
        Vales = new Repository<Vale>(_context);
    }

    public IRepository<Hospital> Hospitales { get; private set; }
    public IRepository<Paciente> Pacientes { get; private set; }
    public IRepository<Expediente> Expedientes { get; private set; }
    public IRepository<Medicamento> Medicamentos { get; private set; }
    public IRepository<Receta> Recetas { get; private set; }
    public IRepository<Vale> Vales { get; private set; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
