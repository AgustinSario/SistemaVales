namespace SistemaVales.Models;

public class Vale
{
    public int Id { get; set; }
    public string NumeroVale { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public DateTime FechaEmision { get; set; } = DateTime.Now;
    public string Estado { get; set; } = "Iniciado"; // Iniciado, Proceso, Entregado
    public string? EntregadoPor { get; set; }
    
    // FK a Expediente
    public int ExpedienteId { get; set; }
    public Expediente? Expediente { get; set; }

    // FK a Medicamento (opcional, para saber qué se entregó en este vale específico)
    public int? MedicamentoId { get; set; }
    public Medicamento? Medicamento { get; set; }
}
