using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Client.Services.ComentarioService
{
    public interface IComentarioService
    {

        Task AgregarComentario(Comentario comentario);
        Task<List<Comentario>> ComentariosProducto(int id);
        Task<List<Usuario>> ComentariosUser(int id);
        Task<bool> ComprobarComentario(int idUser, int idProducto);
        Task BorrarComentarioProducto(int id);
        Task BorrarComentarioUser(int id);
    }
}
