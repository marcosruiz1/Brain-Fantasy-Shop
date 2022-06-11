using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="Debes seleccionar una categoría")]
        public int Categoria { get; set; }
        public decimal Precio { get; set; }
        public string? Descripcion { get; set; }
        public string Imagen { get; set; }
        public bool IsVIP { get; set; } = false;
        public string? Reseñas { get; set; }

        public Producto()
        {
        }

        public Producto(int id, string nombre, int categoria, int precio, string? descripcion, string imagen, string? reseñas)
        {
            Id = id;
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
            Descripcion = descripcion;
            Imagen = imagen;
            Reseñas = reseñas;
        }
        public Producto(string nombre, int categoria, int precio, string? descripcion, string imagen)
        {
            Nombre = nombre;
            Categoria = categoria;
            Precio = precio;
            Descripcion = descripcion;
            Imagen = imagen;
        }
    }
}
