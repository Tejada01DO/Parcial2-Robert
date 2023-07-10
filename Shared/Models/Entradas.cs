using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Entradas
{
    [Key]
    public int EntradaId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
    public string? Concepto { get; set; }
    public int PesoTotal { get; set; }
    public int ProductoId { get; set; }
    public int CaantidadProducida { get; set; }
    public List<EntradasDetalle> entradasDetalle { get; set; } = new List<EntradasDetalle>();
}