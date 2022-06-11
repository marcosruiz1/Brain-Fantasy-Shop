using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models.Dto.ProductoVIP
{
    public class ProductoVIPAltaDto
    {
        public string? Nombre { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        [Required]
        public decimal PrecioFinal { get; set; }
        public string? Descripcion { get; set; }
        [Required]
        public int Categoria { get; set; }
        public string? Imagen { get; set; }
        public string? Estado { get; set; }
        public int? IdUsuario { get; set; }
    }
}
