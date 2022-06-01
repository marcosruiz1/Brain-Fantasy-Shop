using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PracticaBlazor.UI.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using PracticaBlazor.UI.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using PracticaBlazor.UI.Server.Tools;
using Microsoft.AspNetCore.Identity;
using PracticaBlazor.UI.Shared.Models.Email;

namespace PracticaBlazor.UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static Usuario user = new Usuario();
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;

        public AuthController(IConfiguration configuration, AppDbContext context, IEmailSender emailSender)
        {
            _configuration = configuration;
            _context = context;
            _emailSender = emailSender;

        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UsuarioDto request)
        {
            string token = "";
            request.Password = Utilities.Encrypt(request.Password);
            Usuario user = await _context.Usuario.Where(u => u.Username == request.Username && u.Password == request.Password).FirstOrDefaultAsync();
            if (user != null)
            {
                token = CreateToken(user);
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
            _context.Usuario.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuario", new { id = user.Id }, user);
        }


        private string CreateToken(Usuario user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Rol),

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


        //FORGOT PASSWORD
        [HttpPost("enviarEmail")]
        public async Task<ActionResult<ForgotPassword>> ForgotPassword(ForgotPassword forgotPasswordModel)
        {
            string message;
            /*if (!ModelState.IsValid)
                return View(forgotPasswordModel);*/
            Console.WriteLine("sadsad");
            var user = _context.Usuario.Where(u => u.Email == forgotPasswordModel.Email).FirstOrDefault();
            /*if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));*/
            var token = CreateToken(user);
            var resetUrl = $"{Request.Headers["origin"]}/Account/reset-password?token={token}";
            message = $@"<p>Please click the below link to reset your password, the link will be valid for 1 day:</p>
                            <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
 
            var emailCompleto = new Message(new string[] { user.Email }, "Reset password token", message);
            await _emailSender.SendEmailAsync(emailCompleto);
            user.ResetToken = token;
            user.ResetTokenExpires = DateTime.UtcNow.AddDays(1);
            _context.Usuario.Update(user);
            _context.SaveChanges();
            return Ok();
        }

        //RESET PASSWORD
        [HttpPost("resetPassword")]
        public async Task<ActionResult<ResetPassword>> ResetPassword(ResetPassword model)
        {
            var account = getAccountByResetToken(model.Token);
            if(account != null)
            {
                // update password and remove reset token
                model.Password = Utilities.Encrypt(model.Password);
                account.Password = model.Password;
                account.ResetToken = null;
                account.ResetTokenExpires = null;
                _context.Usuario.Update(account);
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }

                         
        }

        private Usuario getAccountByResetToken(string token)
        {
            var account = _context.Usuario.SingleOrDefault(x =>
                x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow);
            return account; 
            
        }
        



    }
}
