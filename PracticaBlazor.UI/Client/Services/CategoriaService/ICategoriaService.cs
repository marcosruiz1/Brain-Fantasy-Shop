using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Client.Services.CategoriaService
{
    public interface ICategoriaService
    {
        Task<string> GetCategoriaNombre(int id);
        Task<List<Categoria>> GetCategoriaas();
    }
}
