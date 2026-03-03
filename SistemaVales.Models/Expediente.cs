namespace SistemaVales.Models;

public class Expediente
{
    public int Id { get; set; }
    public string NumeroExpediente { get; set; } = string.Empty;
    public DateTime FechaInicio { get; set; } = DateTime.Now;
    
    // FK a Paciente
    public int PacienteId { get; set; }
    public Paciente? Paciente { get; set; }
    
    // Documentación requerida según diagrama
    public bool InformeSocialEconomico { get; set; }
    public bool HistoriaClinica { get; set; }
    public bool TieneRecetaFisica { get; set; }
    
    public string Estado { get; set; } = "Iniciado"; // Iniciado, PendienteInformeTecnico, EnAuditoria, Favorable, Desfavorable, EnCompras, ValeGenerado

    // Relación con Receta
    public int? RecetaId { get; set; }
    public Receta? Receta { get; set; }

    // Campos de Informe Técnico (Farmacia)
    public bool? CumpleFormularioTerapeutico { get; set; }
    public bool? InformeOT { get; set; }
    public string? ObservacionesFarmacia { get; set; }

    // Campos de Auditoría Médica
    public string? ResultadoAuditoria { get; set; } // Favorable / Desfavorable

    // Relaciones
    public Vale? Vale { get; set; }
}
