using Microsoft.AspNetCore.Components.Authorization;
using PracticaBlazor.UI.Shared.Models;
using PracticaBlazor.UI.Shared.Models.Dto.Producto;
using System.Net.Http.Json;
using System.Security.Claims;

namespace PracticaBlazor.UI.Client.Services.CarroService
{
    public class CarroService : ICarroService
    {
        private HttpClient Http;
        private List<Carrito> _carritosUser = new();
        private List<Producto> _carritosProd = new();
        private string userId;
        private Carrito _carritoActual = new();
        private bool _comprobador = false;
        private int _numCarrito = 0;

        public event Action CarritoChanged;

        public CarroService(HttpClient Http)
        {
            this.Http = Http;
        }

        public async Task<int> ContadorCarrito(AuthenticationState authState)
        {
            _numCarrito = 0;
            //GET user Id
            if (authState.User.Identity.IsAuthenticated)
            {
                userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userId != null)
                {
                    _carritosUser = await Http.GetFromJsonAsync<List<Carrito>>($"/api/Carritos/User/{userId}");
                }
                foreach (var carrito in _carritosUser)
                {
                    _numCarrito += carrito.numProductos;
                }
            }
            CarritoChanged?.Invoke();
            return _numCarrito;
        }
        public async Task<decimal> CalcularPrecioCarrito(List<Producto> carritosProd, List<Carrito> carritosUser)
        {
            decimal totalPrecio = 0;
            for (int i = 0; i < carritosProd.Count; i++)
            {
                totalPrecio += carritosProd[i].Precio * carritosUser[i].numProductos;
            }
            return totalPrecio;
        }


        public async Task AgregarCarrito(int id, AuthenticationState authState)
        {
            userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            // código para añadir el artículo
            List<Carrito> carritos = await Http.GetFromJsonAsync<List<Carrito>>("/api/Carritos");
            if (carritos.Count > 0)
            {
                foreach (var carrito in carritos)
                {
                    if (carrito.idProducto == id && carrito.idUsuario == Convert.ToInt64(userId))
                    {
                        //Actualiza el carrito
                        carrito.numProductos++;
                        await Http.PutAsJsonAsync<Carrito>($"/api/Carritos/{carrito.Id}", carrito);
                        _comprobador = true;
                    }
                }
            }

            if (!_comprobador)
            {
                _carritoActual.idProducto = id;
                _carritoActual.idUsuario = Int32.Parse(userId);
                await Http.PostAsJsonAsync($"/api/Carritos/", _carritoActual);
            }
            _numCarrito = await ContadorCarrito(authState);
            _comprobador = false;
            CarritoChanged?.Invoke();
        }
        public async Task BorrarCarrito(int idCarrito, AuthenticationState authState)
        {
            await Http.DeleteAsync($"/api/Carritos/{idCarrito}");
            _numCarrito = await ContadorCarrito(authState);
            CarritoChanged?.Invoke();
        }
        public async Task BorrarCarritoProd(int idCarrito)
        {
            await Http.DeleteAsync($"/api/Carritos/prod/{idCarrito}");
        }
        public async Task BorrarCarritoUser(int idCarrito)
        {
            await Http.DeleteAsync($"/api/Carritos/user/{idCarrito}");
        }
        public async Task AumentarNumCarrito(Carrito carrito, AuthenticationState authState)
        {
            carrito.numProductos++;
            await Http.PutAsJsonAsync($"/api/Carritos/{carrito.Id}", carrito);
            _numCarrito = await ContadorCarrito(authState);
            CarritoChanged?.Invoke();
        }

        public async Task DisminuirNumCarrito(Carrito carrito, AuthenticationState authState)
        {
            if(carrito.numProductos == 1)
            {
                await BorrarCarrito(carrito.Id, authState);
            }
            else if(carrito.numProductos > 1)
            {
                carrito.numProductos = carrito.numProductos - 1;
                await Http.PutAsJsonAsync($"/api/Carritos/{carrito.Id}", carrito);
            }
            _numCarrito = await ContadorCarrito(authState);
            CarritoChanged?.Invoke();

        }

        public int TotalArticulos()
        {
            return _numCarrito;
        }
        public void ResetTotalArticulos()
        {
             _numCarrito = 0;
            CarritoChanged?.Invoke();
        }

        public async Task<List<Carrito>> GetCarritoUser(int userId)
        {
            return await Http.GetFromJsonAsync<List<Carrito>>($"/api/Carritos/User/{userId}");

        }
        public async Task<List<Producto>> GetCarritoProd(int userId)
        {
            return await Http.GetFromJsonAsync<List<Producto>>($"/api/Carritos/Prod/{userId}");

        }
        public async Task<List<ProductoCarritoDto>> GetProductoCarritoCompleto(int userId)
        {
            List<Producto> productos = await GetCarritoProd(userId);
            List<Carrito> productosCarr = await GetCarritoUser(userId);
            List<ProductoCarritoDto> productoCompleto = new();

            for (int i = 0; i < productosCarr.Count; i++)
            {
                productoCompleto.Add(new ProductoCarritoDto
                {
                    Nombre = productos[i].Nombre,
                    Precio = productos[i].Precio,
                    Imagen = productos[i].Imagen,
                    Cantidad = productosCarr[i].numProductos
                }); ;
            }
            
            return productoCompleto;

        }

        public async Task<string> Checkout(int id)
        {
            var result = await Http.PostAsJsonAsync("/api/pago/checkout", await GetProductoCarritoCompleto(id));
            var url = await result.Content.ReadAsStringAsync();
            return url;
        }
    }
}
