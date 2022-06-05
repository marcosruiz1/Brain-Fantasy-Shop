using PracticaBlazor.UI.Shared.Models;
using System.Net.Http.Json;

namespace PracticaBlazor.UI.Client.Services.CategoriaService
{
    public class CategoriaService : ICategoriaService
    {
        private HttpClient Http;
        public CategoriaService(HttpClient Http)
        {
            this.Http = Http;
        }

        public async Task<string> GetCategoriaNombre(int id)
        {
            Categoria categoria =  await Http.GetFromJsonAsync<Categoria>($"/api/Categorias/{id}");
            return categoria.Nombre;
        }

        public async Task<List<Categoria>> GetCategoriaas()
        {
            return await Http.GetFromJsonAsync<List<Categoria>>($"/api/Categorias");
        }


    }
}
