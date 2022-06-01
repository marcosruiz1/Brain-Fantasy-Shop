using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Client.Services.UsuarioService
{
    public interface IUsuarioService
    {
        Task RegistrarUsuario(Usuario user);
    }
}
