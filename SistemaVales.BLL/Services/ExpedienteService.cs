using SistemaVales.DAL.UnitOfWork;
using SistemaVales.Models;

namespace SistemaVales.BLL.Services;

public class ExpedienteService : IExpedienteService
{
    private readonly IUnitOfWork _unitOfWork;

    public ExpedienteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Expediente>> ObtenerTodosAsync()
    {
        return await _unitOfWork.Expedientes.GetAllAsync();
    }

    public async Task<Expediente?> ObtenerPorIdAsync(int id)
    {
        return await _unitOfWork.Expedientes.GetByIdAsync(id);
    }

    public async Task CrearExpedienteAsync(Expediente expediente)
    {
        expediente.FechaInicio = DateTime.Now;
        expediente.Estado = "Iniciado";
        
        await _unitOfWork.Expedientes.AddAsync(expediente);
        await _unitOfWork.CompleteAsync();
    }

    public async Task IniciarDesdeRecetaAsync(int recetaId, string numeroExpediente)
    {
        var receta = await _unitOfWork.Recetas.GetByIdAsync(recetaId);
        if (receta == null) throw new Exception("Receta no encontrada");

        var expediente = new Expediente
        {
            NumeroExpediente = numeroExpediente,
            PacienteId = receta.PacienteId,
            RecetaId = receta.Id,
            FechaInicio = DateTime.Now,
            Estado = "PendienteInformeTecnico",
            TieneRecetaFisica = true,
            InformeSocialEconomico = !string.IsNullOrEmpty(receta.InformeSocialEconomico)
        };

        await _unitOfWork.Expedientes.AddAsync(expediente);
        await _unitOfWork.CompleteAsync();
    }

    public async Task ProcesarInformeTecnicoAsync(int expedienteId, bool cumpleFormulario, bool? informeOT, string observaciones)
    {
        var expediente = await _unitOfWork.Expedientes.GetByIdAsync(expedienteId);
        if (expediente == null) return;

        expediente.CumpleFormularioTerapeutico = cumpleFormulario;
        expediente.InformeOT = informeOT;
        expediente.ObservacionesFarmacia = observaciones;

        // Lógica de ruteo
        if (cumpleFormulario)
        {
            if (informeOT == true)
            {
                expediente.Estado = "EnCompras";
            }
            else
            {
                // Si cumple formulario pero no Informe OT, lo mandamos a auditoria o lo dejamos pendiente?
                // El usuario dice: SI = Informe O.T. favorable -> compras.
                // Si es SI pero OT no es favorable, lo mandamos a Auditoria por precaución?
                // "si es SI= Informe O.T. si es favorable tiene que ir al sisterma de compras"
                // Asumo que si OT es NO, va a Auditoria.
                expediente.Estado = "EnAuditoria";
            }
        }
        else
        {
            expediente.Estado = "EnAuditoria";
        }

        _unitOfWork.Expedientes.Update(expediente);
        await _unitOfWork.CompleteAsync();
    }

    public async Task ProcesarAuditoriaMedicaAsync(int expedienteId, string resultado)
    {
        var expediente = await _unitOfWork.Expedientes.GetByIdAsync(expedienteId);
        if (expediente == null) return;

        expediente.ResultadoAuditoria = resultado;
        expediente.Estado = resultado == "Favorable" ? "EnCompras" : "Desfavorable";

        _unitOfWork.Expedientes.Update(expediente);
        await _unitOfWork.CompleteAsync();
    }

    public async Task ActualizarEstadoAsync(int id, string nuevoEstado)
    {
        var expediente = await _unitOfWork.Expedientes.GetByIdAsync(id);
        if (expediente != null)
        {
            expediente.Estado = nuevoEstado;
            _unitOfWork.Expedientes.Update(expediente);
            await _unitOfWork.CompleteAsync();
        }
    }
}
