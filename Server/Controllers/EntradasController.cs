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

            var entrada = await _context.Entradas.FindAsync(EntradaId);

            if(entrada == null)
            {
                return NotFound();
            }
            return entrada;
        }

        [HttpPost]
        public async Task<ActionResult<Entradas>> PostEntradas(Entradas entradas)
        {
            if(!Existe(entradas.EntradaId))
            {
                _context.Entradas.Add(entradas);
            }
            else
            {
                _context.Entradas.Update(entradas);
            }

            await _context.SaveChangesAsync();
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

            _context.Entradas.Remove(entrada);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
    }
}