using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models.Dto.Usuario
{
    public class UsuariorRegisterDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password",
        ErrorMessage = "Both passwords must match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Rol { get; set; } = "ROLE_VISUALIZAR";
        public string? Imagen { get; set; } = "default_User_Img";
    }
}
