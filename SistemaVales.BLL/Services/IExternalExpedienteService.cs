using SistemaVales.BLL.DTOs;

namespace SistemaVales.BLL.Services;

public interface IExternalExpedienteService
{
    Task<ExternalExpedienteDTO?> ConsultarExpedienteAsync(string nroExpediente);
}
