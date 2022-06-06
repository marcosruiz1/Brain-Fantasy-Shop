using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaBlazor.UI.Server.Data;
using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosVIPsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosVIPsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductosVIPs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoVIPs>>> GetProductoVIPs()
        {
          if (_context.ProductoVIPs == null)
          {
              return NotFound();
          }
            return await _context.ProductoVIPs.ToListAsync();
        }
        // GET: api/ProductoVIPs
        [HttpGet("espera")]
        public async Task<ActionResult<List<ProductoVIPs>>> GetProductoVIPEspera()
        {
            List<ProductoVIPs> productosEspera = await _context.ProductoVIPs.Where(u => u.Estado == "ESPERA").ToListAsync();


            return productosEspera;
        }

        // GET: api/ProductoVIPs
        [HttpGet("admitidos")]
        public async Task<ActionResult<List<ProductoVIPs>>> GetProductoVIPAdmitidos()
        {
            List<ProductoVIPs> productosAdmitidos = await _context.ProductoVIPs.Where(u => u.Estado == "ADMITIDO").ToListAsync();

            return productosAdmitidos;
        }

        // GET: api/ProductoVIPs
        [HttpGet("denegados")]
        public async Task<ActionResult<List<ProductoVIPs>>> GetProductoVIPDenegados()
        {
            List<ProductoVIPs> productosDenegados = await _context.ProductoVIPs.Where(u => u.Estado == "DENEGADO").ToListAsync();

            return productosDenegados;
        }

        // GET: api/ProductoVIPs
        [HttpGet("denegados/{id}")]
        public async Task<ActionResult<List<ProductoVIPs>>> GetProductoVIPDenegadosUser(int id)
        {
            List<ProductoVIPs> productosDenegados = await _context.ProductoVIPs.Where(u => u.Estado == "DENEGADO" && u.IdUsuario == id).ToListAsync();

            return productosDenegados;
        }

        // GET: api/ProductoVIPs
        [HttpGet("espera/{id}")]
        public async Task<ActionResult<List<ProductoVIPs>>> GetProductoVIPEsperaUser(int id)
        {
            List<ProductoVIPs> productosEspera = await _context.ProductoVIPs.Where(u => u.Estado == "ESPERA" && u.IdUsuario == id).ToListAsync();


            return productosEspera;
        }

        // GET: api/ProductoVIPs
        [HttpGet("admitidos/{id}")]
        public async Task<ActionResult<List<ProductoVIPs>>> GetProductoVIPAdmitidosUser(int id)
        {
            List<ProductoVIPs> productosAdmitidos = await _context.ProductoVIPs.Where(u => u.Estado == "ADMITIDO" && u.IdUsuario == id).ToListAsync();

            return productosAdmitidos;
        }
        


        // GET: api/ProductosVIPs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoVIPs>> GetProductoVIPs(int id)
        {
          if (_context.ProductoVIPs == null)
          {
              return NotFound();
          }
            var productoVIPs = await _context.ProductoVIPs.FindAsync(id);

            if (productoVIPs == null)
            {
                return NotFound();
            }

            return productoVIPs;
        }

        // PUT: api/ProductosVIPs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductoVIPs(int id, ProductoVIPs productoVIPs)
        {
            if (id != productoVIPs.Id)
            {
                return BadRequest();
            }

            _context.Entry(productoVIPs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoVIPsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductosVIPs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductoVIPs>> PostProductoVIPs(ProductoVIPs productoVIPs)
        {
          if (_context.ProductoVIPs == null)
          {
              return Problem("Entity set 'AppDbContext.ProductoVIPs'  is null.");
          }
            _context.ProductoVIPs.Add(productoVIPs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductoVIPs", new { id = productoVIPs.Id }, productoVIPs);
        }

        // DELETE: api/ProductosVIPs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductoVIPs(int id)
        {
            if (_context.ProductoVIPs == null)
            {
                return NotFound();
            }
            var productoVIPs = await _context.ProductoVIPs.FindAsync(id);
            if (productoVIPs == null)
            {
                return NotFound();
            }

            _context.ProductoVIPs.Remove(productoVIPs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductoVIPsExists(int id)
        {
            return (_context.ProductoVIPs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
