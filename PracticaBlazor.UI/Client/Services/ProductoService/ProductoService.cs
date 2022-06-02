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
    }
}
