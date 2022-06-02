using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Client.Services.ProductoService
{
    public interface IProductoService
    {
        Task<List<Producto>> SearchProducto(string searchText);
        Task CrearProducto(Producto producto);
    }
}
