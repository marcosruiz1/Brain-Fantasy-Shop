using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Client.Services.ProductoService
{
    public interface IProductoService
    {
        Task<List<Producto>> SearchProducto(string searchText);
        Task DeleteProducto(int id);
        Task CrearProducto(Producto producto);
        Task ActualizarProducto(Producto producto);
        Task<List<Producto>> GetProductos();
        Task<Producto> GetProducto(int id);
    }
}
