namespace SistemaVales.Models;

public class Hospital
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    public string Ciudad { get; set; } = string.Empty;

    // Relaciones
    public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
