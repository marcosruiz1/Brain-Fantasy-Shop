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
    public class ComentariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComentariosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Comentarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentario()
        {
          if (_context.Comentario == null)
          {
              return NotFound();
          }
            return await _context.Comentario.ToListAsync();
        }

        // GET: api/Comentarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentario(int id)
        {
          if (_context.Comentario == null)
          {
              return NotFound();
          }
            var comentario = await _context.Comentario.FindAsync(id);

            if (comentario == null)
            {
                return NotFound();
            }

            return comentario;
        }

        // GET: api/Comentarios/producto
        [HttpGet("Producto/{id}")]
        public async Task<ActionResult<List<Comentario>>> GetComentarioProducto(int id)
        {
            return  await _context.Comentario.Where(u => u.idProducto == id).ToListAsync();
        }

        // GET: api/Comentarios/comprobar/{idUser}/{idProducto}
        [HttpGet("comprobar/{idUser}/{idProducto}")]
        public async Task<ActionResult<bool>> ComprobarComentario(int idUser, int idProducto)
        {
            Comentario comentario =  _context.Comentario.Where(u => u.idProducto == idProducto && u.idUsuario == idUser).FirstOrDefault();
            if (comentario is null)
            {
                return true;
            }
            else return false;
        }

        // GET: api/Comentarios/User
        [HttpGet("User/{id}")]
        public async Task<ActionResult<List<Usuario>>> GetComentarioUser(int id)
        {
            List<Comentario> comentarioProd = await _context.Comentario.Where(u => u.idProducto == id).ToListAsync();
            List<Usuario> usuarios = new();

            if (comentarioProd.Count > 0)
            {
                foreach (var comentario in comentarioProd)
                {
                    usuarios.Add(_context.Usuario.Where(u => u.Id == comentario.idUsuario).FirstOrDefault());
                }
            }

            return usuarios;
        }

        // PUT: api/Comentarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(int id, Comentario comentario)
        {
            if (id != comentario.Id)
            {
                return BadRequest();
            }

            _context.Entry(comentario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(id))
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

        // POST: api/Comentarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario(Comentario comentario)
        {
          if (_context.Comentario == null)
          {
              return Problem("Entity set 'AppDbContext.Comentario'  is null.");
          }
            _context.Comentario.Add(comentario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComentario", new { id = comentario.Id }, comentario);
        }

        // DELETE: api/Comentarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            if (_context.Comentario == null)
            {
                return NotFound();
            }
            var comentario = await _context.Comentario.FindAsync(id);
            if (comentario == null)
            {
                return NotFound();
            }

            _context.Comentario.Remove(comentario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Comentarios/prod/5
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpDelete("prod/{id}")]
        public async Task<IActionResult> DeleteComentarioProd(int id)
        {
            List<Comentario> comentarios = _context.Comentario.Where(u => u.idProducto == id).ToList();
            if (comentarios == null)
            {
                return NotFound();
            }

            _context.Comentario.RemoveRange(comentarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Comentarios/user/5
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteComentarioUser(int id)
        {
            List<Comentario> comentarios = _context.Comentario.Where(u => u.idUsuario == id).ToList();
            if (comentarios == null)
            {
                return NotFound();
            }

            _context.Comentario.RemoveRange(comentarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ComentarioExists(int id)
        {
            return (_context.Comentario?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
