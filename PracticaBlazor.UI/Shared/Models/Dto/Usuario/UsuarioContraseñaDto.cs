using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models.Dto.Usuario
{
    public class UsuarioContraseñaDto
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Rol { get; set; } = "ROLE_VISUALIZAR";
        public string? Imagen { get; set; } = "default_User_Img";

        [Required]
		[MinLength(8)]
		public string NuevaContraseña { get; set; }
		[Required]
		public string ActualContraseña { get; set; }
		[Required]
		[Compare("NuevaContraseña",
			ErrorMessage = "Both passwords must match")]
		public string ConfirmaContraseña { get; set; }

	}
}
