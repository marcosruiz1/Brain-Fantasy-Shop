using PracticaBlazor.UI.Shared.Models;
using System.Net.Http.Json;

namespace PracticaBlazor.UI.Client.Services.ProductoService
{
    public class ProductoService : IProductoService
    {
        private HttpClient Http;
        public ProductoService(HttpClient Http)
        {
            this.Http = Http;
        }

        public async Task<List<Producto>> SearchProducto(string searchText)
        {
            return await Http.GetFromJsonAsync<List<Producto>>($"/api/Productos/Search/{searchText}");
        }

        public async Task CrearProducto(Producto producto)
        {
            await Http.PostAsJsonAsync($"api/Productos/", producto);
        }

        public async Task ActualizarProducto(Producto producto)
        {
            await Http.PutAsJsonAsync<Producto>($"/api/Productos/{producto.Id}", producto);
        }
        public async Task<List<Producto>> GetProductos()
        {
            return await Http.GetFromJsonAsync<List<Producto>>($"/api/Productos/");
        }
        public async Task<Producto> GetProducto(int id)
        {
            return await Http.GetFromJsonAsync<Producto>($"/api/Productos/{id}");
        }
        public async Task DeleteProducto(int id)
        {
            await Http.DeleteAsync($"/api/Productos/{id}");
        }
    }
}
