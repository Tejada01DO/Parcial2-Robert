using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Entradas
{
    [Key]
    public int EntradaId { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "El concepto es un campo obligatorio")]
    public string? Concepto { get; set; }

    [Required(ErrorMessage = "El Producto ID es un campo obligatorio")]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "Es necesario especificar la cantidad que se utilizo")]
    public int CantidadProducida { get; set; }
    
    [ForeignKey("EntradaId")]
    public List<EntradasDetalle> entradasDetalle { get; set; } = new List<EntradasDetalle>();
}