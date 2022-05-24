using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models.Services.ProductoService
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
