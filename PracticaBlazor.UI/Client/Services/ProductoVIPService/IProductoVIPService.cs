using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Client.Services.ProductoVIPService
{
    public interface IProductoVIPService
    {
        Task AgregarProductoVIP(ProductoVIPs productoVIP);
        Task<List<ProductoVIPs>> ProductosVIPEspera();
        Task<List<ProductoVIPs>> ProductosVIPAdmitidos();
        Task<List<ProductoVIPs>> ProductosVIPDenegados();
        Task<List<ProductoVIPs>> ProductosVIPEsperaUser(int id);
        Task<List<ProductoVIPs>> ProductosVIPAdmitidosUser(int id);
        Task<List<ProductoVIPs>> ProductosVIPDenegadosUser(int id);
        Task BorrarProductoVIP(int id);
        Task ActualizarProducto(ProductoVIPs productoVIP);
    }
}
