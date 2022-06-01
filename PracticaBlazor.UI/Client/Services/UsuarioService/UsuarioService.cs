using PracticaBlazor.UI.Shared.Models;
using System.Net.Http.Json;

namespace PracticaBlazor.UI.Client.Services.UsuarioService
{
    public class UsuarioService : IUsuarioService
    {
        HttpClient Http;

        public UsuarioService(HttpClient Http)
        {
            this.Http = Http;
        }
        public async Task RegistrarUsuario(Usuario user)
        {
            await Http.PostAsJsonAsync($"api/Usuarios/register/", user);
        }
    }
}
