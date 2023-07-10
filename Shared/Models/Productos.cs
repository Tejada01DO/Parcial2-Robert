using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Productos
{
    [Key]
    public int ProductoId  { get; set; }
    public string? Descripcion { get; set; }
    public double PrecioCompra { get; set; }
    public double PrecioVenta { get; set; }
    public int Existencia { get ;set; }
}