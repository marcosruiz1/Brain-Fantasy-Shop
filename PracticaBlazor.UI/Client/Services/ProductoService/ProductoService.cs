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
    }
}
