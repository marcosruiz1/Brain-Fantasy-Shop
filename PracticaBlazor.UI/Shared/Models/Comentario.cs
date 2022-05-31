using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models
{
    public class Comentario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Mensaje { get; set; }
        public DateTime fecha { get; set;}
        public int idUsuario { get; set; }
        public int idProducto { get; set; }

    }
}
