#nullable disable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticaBlazor.UI.Server.Data;
using PracticaBlazor.UI.Server.Tools;
using PracticaBlazor.UI.Shared.Models;
using PracticaBlazor.UI.Shared.Models.Dto.Usuario;
using PracticaBlazor.UI.Shared.Models.Email;

namespace PracticaBlazor.UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/Usuarios
        [Authorize(Roles="ROLE_ADMIN")]
        [HttpGet]    
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuario()
        {
            return await _context.Usuario.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {  
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                usuario.Password = Utilities.Encrypt(usuario.Password);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // PUT: api/Usuarios/pwd/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("pwd/{id}")]
        public async Task<IActionResult> PutUsuarioPwd(int id, UsuarioContraseñaDto userPwd)
        {
            bool completado = false;
            Usuario usuario = await _context.Usuario.FindAsync(id);


            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;

           
            try
            {
                
                if (userPwd.ActualContraseña != null)
                {
                    userPwd.ActualContraseña = Utilities.Encrypt(userPwd.ActualContraseña);
                    if (userPwd.ActualContraseña != usuario.Password)
                    {
                         return Ok(completado); 
                    }
                    else
                    {
                        userPwd.NuevaContraseña = Utilities.Encrypt(userPwd.NuevaContraseña);
                        usuario.Password = userPwd.NuevaContraseña;
                        await _context.SaveChangesAsync();
                    }
                }
                         
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // PUT: api/Usuarios/datos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("datos/{id}")]
        public async Task<IActionResult> PutUsuarioDatos(int id, UsuarioDatosDto userDatos)
        {
            bool completado = false;
            Usuario usuario = await _context.Usuario.FindAsync(id);


            if (id != usuario.Id)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;


            try
            {
                usuario.Nombre = userDatos.Nombre;
                usuario.Apellidos = userDatos.Apellidos;
                usuario.Email = userDatos.Email;
                usuario.Telefono = userDatos.Telefono;
                usuario.Direccion = userDatos.Direccion;
                usuario.Password = Utilities.Encrypt(usuario.Password);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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


        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            var usernameExists = _context.Usuario.Where(u => u.Username == usuario.Username).FirstOrDefault();
            var emailExists = _context.Usuario.Where(u => u.Email == usuario.Email).FirstOrDefault();

            if (usernameExists == null)
            {
                if (emailExists == null)
                {
                    usuario.Rol = "ROLE_VISUALIZAR";
                    usuario.Imagen = "default_User_Img";
                    usuario.Password = Utilities.Encrypt(usuario.Password);
                }
            }

            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
            
        }

        // DELETE: api/Usuarios/5
        [Authorize(Roles = "ROLE_ADMIN")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("getMe")]
        [Authorize]
        public ActionResult<Usuario> GetMe()
        {
            string username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
            Usuario usuario = _context.Usuario.Where(u => u.Username == username).FirstOrDefault();
            return usuario;
        }


        private bool UsuarioExists(int id)
        {
            return _context.Usuario.Any(e => e.Id == id);
        }

        


    }
}
