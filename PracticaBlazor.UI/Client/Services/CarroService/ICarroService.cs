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
        Task BorrarCarritoProd(int idCarrito);
        Task BorrarCarritoUser(int idCarrito);
        int TotalArticulos();
        void ResetTotalArticulos();
        Task<int> ContadorCarrito(AuthenticationState authState);
        Task<decimal> CalcularPrecioCarrito(List<Producto> carritosProd, List<Carrito> carritosUser);
        Task<List<Carrito>> GetCarritoUser(int userId);
        Task<List<Producto>> GetCarritoProd(int userId);
        Task DisminuirNumCarrito(Carrito carrito, AuthenticationState authState);
        Task AumentarNumCarrito(Carrito carrito, AuthenticationState authState);
        Task<string> Checkout(int id);
    }
}
