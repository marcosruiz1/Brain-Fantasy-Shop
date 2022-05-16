using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models.Dto.Producto
{
    [Keyless]
    public class ProductoCarritoDto
    {      
        public string Nombre { get; set; }
        public int Categoria { get; set; }
        public int Precio { get; set; }
        public string? Descripcion { get; set; }
        public string Imagen { get; set; }
    }
}
