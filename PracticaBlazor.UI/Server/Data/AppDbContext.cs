 using Microsoft.EntityFrameworkCore;
using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        //Referenciamos los modelos que vaya a utilizar Entity Framework. 
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Carrito> Carrito { get; set; }
        public virtual DbSet<ProductoVIP> ProductoVIP { get; set; }
    }
}
