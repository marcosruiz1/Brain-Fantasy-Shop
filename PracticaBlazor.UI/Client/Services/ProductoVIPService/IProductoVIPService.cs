using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Client.Services.ProductoVIPService
{
    public interface IProductoVIPService
    {
        Task AgregarProductoVIP(ProductoVIPs productoVIP);
        Task<List<ProductoVIPs>> ProductosVIPEspera();
        Task<List<ProductoVIPs>> ProductosVIPAdmitidos();
        Task<List<ProductoVIPs>> ProductosVIPDenegados();
        Task BorrarProductoVIP(int id);
        Task ActualizarProducto(ProductoVIPs productoVIP);
    }
}
