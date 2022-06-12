using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models.Dto.Usuario
{
    public class UsuarioRegistroAdminDto
    {
        [Required]
        [MinLength(4)]
        public string Username { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Tiene que ser un número natural")]
        [Required]
        public string Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Rol { get; set; } = "ROLE_VISUALIZAR";
        public string? Imagen { get; set; } = "default_User_Img";

    }
}
