using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
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
        [NotMapped]
        [Compare("Password",
        ErrorMessage = "Both passwords must match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Tiene que ser un número natural")]
        public string Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Rol { get; set; } = "ROLE_VISUALIZAR";
        public string? Imagen { get; set; } = "default_User_Img";
        //TokenPassword
        public string? ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

    }
}
