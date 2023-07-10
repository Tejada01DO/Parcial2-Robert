using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{
    public DbSet<Entradas> Entradas { get; set; }
    public DbSet<Productos> Productos { get; set; }
    
    public Context(DbContextOptions<Context> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Productos>().HasData
        (
            new Productos
            {
                ProductoId = 1,
                Descripcion = "Mani",
                PrecioCompra = 8,
                PrecioVenta = 15,
                Existencia = 250
            },

            new Productos
            {
                ProductoId = 2,
                Descripcion = "Pistachos",
                PrecioCompra = 15,
                PrecioVenta = 30,
                Existencia = 300
            },

            new Productos
            {
                ProductoId = 3,
                Descripcion = "Pasas",
                PrecioCompra = 10,
                PrecioVenta = 25,
                Existencia = 130
            },

            new Productos
            {
                ProductoId = 4,
                Descripcion = "Ciruelas",
                PrecioCompra = 25,
                PrecioVenta = 50,
                Existencia = 350
            },

            new Productos
            {
                ProductoId = 5,
                Descripcion = "Mixto MPP",
                PrecioCompra = 30,
                PrecioVenta = 60,
                Existencia = 320
            },

            new Productos
            {
                ProductoId = 6,
                Descripcion = "Mixto MPC",
                PrecioCompra = 30,
                PrecioVenta = 60,
                Existencia = 310
            },

            new Productos
            {
                ProductoId = 7,
                Descripcion = "Mixto MPP",
                PrecioCompra = 25,
                PrecioVenta = 50,
                Existencia = 250
            }
        );
    }
}