using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class EntradasDetalle
{
    [Key]
    public int DetalleId { get; set; }
    public int EntradaId { get; set; }

    [Required(ErrorMessage = "Es Producto ID es un campo obligatorio")]
    public int ProductoId { get; set; }

    [Required(ErrorMessage = "Es necesario especificar la cantidad utilizada")]
    public int CantidadUtilizada { get; set; }
}