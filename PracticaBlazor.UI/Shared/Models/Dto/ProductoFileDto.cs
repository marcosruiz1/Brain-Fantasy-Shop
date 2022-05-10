using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models.Dto
{
    public  class ProductoFileDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Categoria { get; set; }
        public int Precio { get; set; }
        public string? Descripcion { get; set; }
        public IFormFile Imagen { get; set; }
        public string? Reseñas { get; set; }
    }
}
