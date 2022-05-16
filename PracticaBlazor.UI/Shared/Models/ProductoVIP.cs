using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models
{
    public class ProductoVIP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        public int Categoria { get; set; }
        public int Precio { get; set; }
        public string? Descripcion { get; set; }
        public string Imagen { get; set; }

        public int IdUser { get; set; }
        public Usuario Usuario { get; set; }
    }
}
