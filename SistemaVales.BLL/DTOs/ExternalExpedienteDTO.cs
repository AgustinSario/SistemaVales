using System.Xml.Serialization;

namespace SistemaVales.BLL.DTOs;

[XmlRoot("respuesta")]
public class ExternalExpedienteDTO
{
    [XmlElement("extracto")]
    public string Extracto { get; set; } = string.Empty;

    [XmlElement("fecha")]
    public string Fecha { get; set; } = string.Empty;

    [XmlElement("fecha_utc")]
    public string FechaUtc { get; set; } = string.Empty;

    [XmlElement("numero_expediente")]
    public string NumeroExpediente { get; set; } = string.Empty;

    [XmlElement("ubicacion")]
    public string Ubicacion { get; set; } = string.Empty;

    [XmlElement("ubicacion_desde")]
    public string UbicacionDesde { get; set; } = string.Empty;

    [XmlElement("ubicacion_tiempo_transcurrido")]
    public string UbicacionTiempoTranscurrido { get; set; } = string.Empty;

    [XmlElement("error")]
    public string Error { get; set; } = string.Empty;

    [XmlElement("estado")]
    public string Estado { get; set; } = string.Empty;
}
