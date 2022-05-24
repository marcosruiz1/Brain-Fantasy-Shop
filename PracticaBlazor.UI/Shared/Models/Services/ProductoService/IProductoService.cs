using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaBlazor.UI.Shared.Models.Services.ProductoService
{
    public interface IProductoService
    {
        Task<List<Producto>> SearchProducto(string searchText);
    }
}
