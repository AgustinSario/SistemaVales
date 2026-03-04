namespace SistemaVales.Web.Models;

public class DashboardViewModel
{
    // Expedientes
    public int ExpedientesCreados { get; set; }
    public int ExpedientesEnCompras { get; set; }
    
    // Vales
    public int ValesCreados { get; set; }
    public int ValesEntregados { get; set; }
    
    // Recetas
    public int RecetasCreadas { get; set; }
    public int RecetasEvaluadas { get; set; }
}
