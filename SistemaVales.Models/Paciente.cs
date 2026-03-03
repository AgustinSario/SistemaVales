namespace SistemaVales.Models;

public class Paciente
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string DNI { get; set; } = string.Empty;
    public string Direccion { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
    // FK a Hospital
    public int HospitalId { get; set; }
    public Hospital? Hospital { get; set; }
    
    // Relaciones
    public ICollection<Expediente> Expedientes { get; set; } = new List<Expediente>();
}
