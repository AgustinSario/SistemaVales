using System.ComponentModel.DataAnnotations;

namespace SistemaVales.Models;

public class Receta
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "DNI")]
    public string DNI { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Informe Social Económico")]
    public string InformeSocialEconomico { get; set; } = string.Empty;

    [Display(Name = "¿Tiene Obra Social?")]
    public bool TieneObraSocial { get; set; }

    [Display(Name = "Análisis Clínicos")]
    public string AnalisisClinicos { get; set; } = string.Empty;

    [Display(Name = "Monto Total Estimado")]
    public decimal MontoTotal { get; set; }

    [Display(Name = "¿Es por Asistencia a la Comunidad?")]
    public bool EsAsistenciaComunidad { get; set; }

    [Display(Name = "Imagen de la Receta")]
    public string? ImagenUrl { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Required]
    public int PacienteId { get; set; }
    public Paciente? Paciente { get; set; }

    [Required]
    public int HospitalId { get; set; }
    public Hospital? Hospital { get; set; }
}
