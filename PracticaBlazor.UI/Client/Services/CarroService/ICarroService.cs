using Microsoft.AspNetCore.Components.Authorization;
using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Client.Services.CarroService
{
    public interface ICarroService
    {
        event Action CarritoChanged;

        Task AgregarCarrito(int id, AuthenticationState authState);
        //Task GetCarrito(List<Carrito> carritosUser, List<Producto> carritosProd, int userId);
        Task BorrarCarrito(int idCarrito, AuthenticationState authState);
        int TotalArticulos();
        Task<int> ContadorCarrito(AuthenticationState authState);
        Task<int> CalcularPrecioCarrito(List<Producto> carritosProd, List<Carrito> carritosUser);
        Task<List<Carrito>> GetCarritoUser(int userId);
        Task<List<Producto>> GetCarritoProd(int userId);
    }
}
