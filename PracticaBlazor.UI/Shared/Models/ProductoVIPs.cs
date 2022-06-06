using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models
{
    public class ProductoVIPs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public decimal PrecioMin { get; set; }
        [Required]
        public decimal PrecioMax { get; set; }
        public decimal? PrecioFinal { get; set; }
        public string Descripcion { get; set; }
        public int? Categoria { get; set; }
        public string Imagen { get; set; }
        public string Estado { get; set; } = "ESPERA";
        public int IdUsuario { get; set; }
    }
}
