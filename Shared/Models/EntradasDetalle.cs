using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EntradasDetalle
{
    [Key]

    public int DetalleId { get; set; }
    [ForeignKey("EntradaId")]
    public int EntradaId { get; set; }
    public int ProductoId { get; set; }
    public int Cantidad { get; set; }
}