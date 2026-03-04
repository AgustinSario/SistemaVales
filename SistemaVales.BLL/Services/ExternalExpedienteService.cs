using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;
using SistemaVales.BLL.DTOs;

namespace SistemaVales.BLL.Services;

public class ExternalExpedienteService : IExternalExpedienteService
{
    private readonly HttpClient _httpClient;
    private const string Secret = "0d1e2s3a4r5r6o7l8l9o0s1n2e3a4srl";
    private const string Key = "123456789012345";
    private const string BaseUrl = "http://expedientes.saludcorrientes.gob.ar:9191/";

    public ExternalExpedienteService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ExternalExpedienteDTO?> ConsultarExpedienteAsync(string nroExpediente)
    {
        try
        {
            string nro = nroExpediente.Trim();
            // La API de Salúd Corrientes requiere formato: ####-######-####
            var parts = nro.Split('-');
            if (parts.Length == 3)
            {
                string inst = parts[0].Trim().PadLeft(4, '0');
                string exp = parts[1].Trim().PadLeft(6, '0');
                string ano = parts[2].Trim();
                nro = $"{inst}-{exp}-{ano}";
            }

            string cadena = Secret + "expediente" + nro;
            
            string firma = CalcularHmac(cadena, Secret);

            string url = $"{BaseUrl}api/expediente/{nro}/ubicacion?key={Key}&firma={firma}&format=xml";
            Console.WriteLine($"[ExternalService] Requested URL: {url}");

            var response = await _httpClient.PostAsync(url, null);
            Console.WriteLine($"[ExternalService] Response Status: {response.StatusCode}");
            
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"[ExternalService] Response Body: {responseString}");

            if (responseString.TrimStart().StartsWith("{"))
            {
                try 
                {
                    using var doc = System.Text.Json.JsonDocument.Parse(responseString);
                    var root = doc.RootElement;
                    var estado = root.GetProperty("estado").GetString();
                    
                    if (estado == "ok" && root.TryGetProperty("contenido", out var contenido) && contenido.ValueKind != System.Text.Json.JsonValueKind.Null)
                    {
                        var dto = new ExternalExpedienteDTO
                        {
                            Estado = estado,
                            NumeroExpediente = contenido.TryGetProperty("numeroExpediente", out var n) ? n.GetString() ?? "" : "",
                            Extracto = contenido.TryGetProperty("extracto", out var ex) ? ex.GetString() ?? "" : "",
                            Ubicacion = contenido.TryGetProperty("ubicacion", out var u) ? u.GetString() ?? "" : "",
                            Fecha = contenido.TryGetProperty("fecha", out var f) ? f.GetString() ?? "" : "",
                            FechaUtc = contenido.TryGetProperty("fechaUtc", out var fu) ? fu.GetString() ?? "" : "",
                            UbicacionDesde = contenido.TryGetProperty("ubicacionDesde", out var ud) ? ud.GetString() ?? "" : "",
                            UbicacionTiempoTranscurrido = contenido.TryGetProperty("ubicacionTiempoTranscurrido", out var ut) ? ut.GetString() ?? "" : ""
                        };
                        return dto;
                    }
                    else if (root.TryGetProperty("error", out var errorNode) && errorNode.ValueKind != System.Text.Json.JsonValueKind.Null)
                    {
                        var msg = errorNode.TryGetProperty("mensaje", out var m) ? m.GetString() : "Error desconocido";
                        return new ExternalExpedienteDTO { Error = msg ?? "Error desconocido" };
                    }
                }
                catch (Exception ex)
                {
                    return new ExternalExpedienteDTO { Error = $"Error al procesar JSON: {ex.Message}" };
                }
            }

            if (!response.IsSuccessStatusCode) 
            {
                return new ExternalExpedienteDTO { Error = $"HTTP Error {response.StatusCode}" };
            }
            
            var result = DeserializeXml(responseString);
            if (result == null) 
            {
                return new ExternalExpedienteDTO { Error = $"No se pudo procesar la respuesta." };
            }
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ExternalService] Exception: {ex.Message}");
            return new ExternalExpedienteDTO { Error = $"Excepción: {ex.Message}" };
        }
    }

    private string CalcularHmac(string data, string secret)
    {
        var keyBytes = Encoding.UTF8.GetBytes(secret);
        var dataBytes = Encoding.UTF8.GetBytes(data);

        using var hmac = new HMACSHA256(keyBytes);
        var hashBytes = hmac.ComputeHash(dataBytes);
        
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    private ExternalExpedienteDTO? DeserializeXml(string xml)
    {
        var serializer = new XmlSerializer(typeof(ExternalExpedienteDTO));
        using var reader = new StringReader(xml);
        return (ExternalExpedienteDTO?)serializer.Deserialize(reader);
    }
}
