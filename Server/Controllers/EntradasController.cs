using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Parcial2_Robert.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EntradasController : ControllerBase
    {
        private readonly Context _context;

        public EntradasController(Context context)
        {
            _context = context;
        }

        public bool Existe(int EntradaId)
        {
            return (_context.Entradas?.Any(e => e.EntradaId == EntradaId)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entradas>>> Obtener()
        {
            if(_context.Entradas == null)
            {
                return NotFound();
            }
            else
            {
                return await _context.Entradas.ToListAsync();
            }
        }

        [HttpGet("{EntradaId}")]
        public async Task<ActionResult<Entradas>> ObtenerEntradas(int EntradaId)
        {
            if(_context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas.Include(e => e.entradasDetalle).Where( e => e.EntradaId == EntradaId).FirstOrDefaultAsync();

            if(entrada == null)
            {
                return NotFound();
            }

            foreach(var item in entrada.entradasDetalle)
            {
                Console.WriteLine($"{item.DetalleId}, {item.EntradaId}, {item.ProductoId}, {item.CantidadUtilizada}");
            }

            return entrada;
        }
        
        [HttpPost]
        public async Task<ActionResult<Entradas>> PostEntradas(Entradas entradas)
        {
            if(!Existe(entradas.EntradaId))
            {
                Productos? producto = new Productos();
                foreach(var productoConsumido in entradas.entradasDetalle)
                {
                    producto = _context.Productos.Find(productoConsumido.ProductoId);

                    if(producto != null)
                    {
                        producto.Existencia -= productoConsumido.CantidadUtilizada;
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();
                        _context.Entry(producto).State = EntityState.Detached;
                    }
                }
                await _context.Entradas.AddAsync(entradas);
            }
            else
            {
                var entradaAnterior = _context.Entradas.Include(e => e.entradasDetalle).AsNoTracking()
                .FirstOrDefault(e => e.EntradaId == entradas.EntradaId);

                Productos? producto = new Productos();

                if(entradaAnterior != null && entradaAnterior.entradasDetalle != null)
                {
                    foreach(var productoConsumido in entradaAnterior.entradasDetalle)
                    {
                        if(productoConsumido != null)
                        {
                            producto = _context.Productos.Find(productoConsumido.ProductoId);

                            if(producto != null)
                            {
                                producto.Existencia +=  productoConsumido.CantidadUtilizada;
                                _context.Productos.Update(producto);
                                await _context.SaveChangesAsync();
                                _context.Entry(producto).State = EntityState.Detached;
                            }   
                        }
                    }
                }

                if(entradaAnterior != null)
                {
                    producto = _context.Productos.Find(entradaAnterior.ProductoId);

                    if(producto != null)
                    {
                        producto.Existencia -= entradaAnterior.CantidadProducida;
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();
                        _context.Entry(producto).State = EntityState.Detached;
                    }
                }

                _context.Database.ExecuteSqlRaw($"Delete from entradasDetalle where EntradaId = {entradas.EntradaId}");

                foreach(var productoConsumido in entradas.entradasDetalle)
                {
                    producto = _context.Productos.Find(productoConsumido.ProductoId);

                    if(producto != null)
                    {
                        producto.Existencia -= productoConsumido.CantidadUtilizada;
                        _context.Productos.Update(producto);
                        await _context.SaveChangesAsync();
                        _context.Entry(producto).State = EntityState.Detached;
                        _context.Entry(productoConsumido).State = EntityState.Added;
                    }
                }

                producto = _context.Productos.Find(entradas.ProductoId);

                if(producto != null)
                {
                    producto.Existencia += entradas.CantidadProducida;
                    _context.Productos.Update(producto);
                    await _context.SaveChangesAsync();
                    _context.Entry(producto).State = EntityState.Detached;
                }
                _context.Entradas.Update(entradas);
            }

            await _context.SaveChangesAsync();
            _context.Entry(entradas).State = EntityState.Detached;
            return Ok(entradas);
        }

        [HttpDelete("{EntradaId}")]
        public async Task<IActionResult> EliminarEntrada(int EntradaId)
        {
            if(_context.Entradas == null)
            {
                return NotFound();
            }

            var entrada = await _context.Entradas.FindAsync(EntradaId);

            if(entrada == null)
            {
                return NotFound();
            }

            Productos? producto = new Productos();

            foreach(var productoConsumido in entrada.entradasDetalle)
            {
                producto = _context.Productos.Find(productoConsumido.ProductoId);

                if(producto != null)
                {
                    producto.Existencia -= productoConsumido.CantidadUtilizada;
                    _context.Entry(producto).State = EntityState.Modified;
                }
            }

            producto = _context.Productos.Find(entrada.ProductoId);

            if(producto != null)
            {
                producto.Existencia -= entrada.CantidadProducida;
                _context.Entry(producto).State = EntityState.Modified; 
            }
            _context.Database.ExecuteSqlRaw($"Delete from entradasDetalle where EntradaId = {entrada.EntradaId}");

            _context.Entradas.Remove(entrada);
            await _context.SaveChangesAsync();
            return NoContent();
        } 
    }
}   