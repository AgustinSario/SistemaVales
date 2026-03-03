namespace SistemaVales.Models;

public class Medicamento
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Cantidad { get; set; } = string.Empty;
    public string Unidad { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal MontoEstimado { get; set; }
}
