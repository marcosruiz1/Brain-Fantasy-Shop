using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PracticaBlazor.UI.Server.Services.UserServices;
using PracticaBlazor.UI.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PracticaBlazor.UI.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using PracticaBlazor.UI.Server.Tools;

namespace PracticaBlazor.UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Usuario user = new Usuario();
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AuthController(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;

        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UsuarioDto request)
        {
            string token = "";
            request.Password = Utilities.Encrypt(request.Password);
            Usuario user = await _context.Usuario.Where(u => u.Username == request.Username && u.Password == request.Password).FirstOrDefaultAsync();
            if (user != null)
            {
                token = CreateToken(request, user.Rol, user.Id);
            }
            return Ok(token);

        }
        [HttpPost("register")]
        public async Task<ActionResult<Usuario>> Register(Usuario user)
        {
            var usernameExists = _context.Usuario.Where(u => u.Username == user.Username).FirstOrDefault();
            var emailExists = _context.Usuario.Where(u => u.Email == user.Email).FirstOrDefault();
            if (usernameExists == null)
            {
                if (emailExists == null)
                {
                    user.Rol = "ROLE_VISUALIZAR";
                    user.Imagen = "default_User_Img";
                    user.Password = Utilities.Encrypt(user.Password);
                }
            }
            return Ok(user);
        }


        private string CreateToken(UsuarioDto user, string role, int id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Role, role),

            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


    }
}
