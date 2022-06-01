using PracticaBlazor.UI.Shared.Models;
using System.Net.Http.Json;

namespace PracticaBlazor.UI.Client.Services.ComentarioService
{
    public class ComentarioService : IComentarioService
    {
        HttpClient Http;

        public ComentarioService(HttpClient Http)
        {
            this.Http = Http;
        }

        public async Task AgregarComentario(Comentario comentario)
        {
            await Http.PostAsJsonAsync($"/api/Comentarios/", comentario);
        }

        public async Task<List<Comentario>> ComentariosProducto(int id)
        {
            return await Http.GetFromJsonAsync<List<Comentario>>($"/api/Comentarios/Producto/{id}");

        }

        public async Task<List<Usuario>> ComentariosUser(int id)
        {
            return await Http.GetFromJsonAsync<List<Usuario>>($"/api/Comentarios/User/{id}");

        }

        public async Task<bool> ComprobarComentario(int idUser,int idProducto)
        {
            return await Http.GetFromJsonAsync<bool>($"/api/Comentarios/comprobar/{idUser}/{idProducto}");

        }

        public async Task BorrarComentarioProducto(int id)
        {
              await Http.DeleteAsync($"/api/Comentarios/prod/{id}");
        }
        public async Task BorrarComentarioUser(int id)
        {
            await Http.DeleteAsync($"/api/Comentarios/user/{id}");
        }


       
    }
}
