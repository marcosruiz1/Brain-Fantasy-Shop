#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaBlazor.UI.Server.Data;
using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarritosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Carritos
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Carrito>>> GetCarrito()
        {
            return await _context.Carrito.ToListAsync();
        }

        // GET: api/Carritos/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrito>> GetCarrito(int id)
        {
            var carrito = await _context.Carrito.FindAsync(id);

            if (carrito == null)
            {
                return NotFound();
            }

            return carrito;
        }

        // GET: api/Carritos/Prod
        [Authorize]
        [HttpGet("Prod/{id}")]
        public async Task<ActionResult<List<Producto>>> GetCarritoProductos(int id)
        {
            List<Carrito> userCarritos = _context.Carrito.Where(u => u.idUsuario == id).ToList();
            List<Producto> productosCarrito = new();

            if (userCarritos.Count > 0)
            {
                foreach (var carrito in userCarritos)
                {
                    productosCarrito.Add(_context.Producto.Where(u => u.Id == carrito.idProducto).FirstOrDefault());
                }
            }

            return productosCarrito;
        }


        // GET: api/Carritos/User
        [Authorize]
        [HttpGet("User/{id}")]
        public async Task<ActionResult<List<Carrito>>> GetCarritoUser(int id)
        {
            List<Carrito> userCarritos = _context.Carrito.Where(u => u.idUsuario == id).ToList();

            if (userCarritos == null)
            {
                return NotFound();
            }

            return userCarritos;
        }

        // PUT: api/Carritos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrito(int id, Carrito carrito)
        {
            if (id != carrito.Id)
            {
                return BadRequest();
            }

            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoExists(id))
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

        // POST: api/Carritos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Carrito>> PostCarrito(Carrito carrito)
        {
            _context.Carrito.Add(carrito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrito", new { id = carrito.Id }, carrito);
        }

        // DELETE: api/Carritos/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrito(int id)
        {
            var carrito = await _context.Carrito.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carrito.Remove(carrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Carritos/prod/5
        [Authorize]
        [HttpDelete("prod/{id}")]
        public async Task<IActionResult> DeleteCarritoProd(int id)
        {
            List<Carrito> carrito = _context.Carrito.Where(u => u.idProducto == id).ToList();
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carrito.RemoveRange(carrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/Carritos/user/5
        [Authorize]
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteCarritoUser(int id)
        {
            List<Carrito> carrito = _context.Carrito.Where(u => u.idUsuario == id).ToList();
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carrito.RemoveRange(carrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarritoExists(int id)
        {
            return _context.Carrito.Any(e => e.Id == id);
        }
    }
}
