using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using PracticaBlazor.UI.Client;
using PracticaBlazor.UI.Client.Shared;
using Blazored.LocalStorage;
using Blazored.Toast;
using Blazored.Toast.Services;
using PracticaBlazor.UI.Shared.Models;
using System.Security.Claims;
using System.Text.Json;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Graphics;
using System.IO;
using System.Data;

namespace PracticaBlazor.UI.Client.Pages.CarritoCompra
{
    public partial class MostrarCarrito
    {
        private List<Producto> _carritosProd = new();
        private List<Carrito> _carritosUser = new();
        private decimal precioTotal = 0;

        //USER
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        AuthenticationState authState;
        private string userId;

        protected override async Task OnInitializedAsync()
        {
            authState = await authenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await GetCarritos();
            }

            precioTotal = await CarroService.CalcularPrecioCarrito(_carritosProd, _carritosUser);
        }

        public async Task DeleteProdCarrito(int idCarrito)
        {

            await CarroService.BorrarCarrito(idCarrito, authState);
            await GetCarritos();

        }
        public async Task GoProdCarrito(int idProducto)
        {
            Navigation.NavigateTo($"/producto/ver/{idProducto}");

        }
        
        public async Task DisminuirCarrito(Carrito carrito)
        {
            await CarroService.DisminuirNumCarrito(carrito, authState);
            if(carrito.numProductos == 1)
            {
                await GetCarritos();
            }
            await ActualizarProducto();
        }
        public async Task AumentarCarrito(Carrito carrito)
        {
            await CarroService.AumentarNumCarrito(carrito, authState);
            await ActualizarProducto();
        }

        private async Task ActualizarProducto()
        {
            precioTotal = await CarroService.CalcularPrecioCarrito(_carritosProd, _carritosUser);
            StateHasChanged();
        }
        private async Task GetCarritos()
        {
            _carritosUser = await CarroService.GetCarritoUser(Convert.ToInt32(userId));
            _carritosProd = await CarroService.GetCarritoProd(Convert.ToInt32(userId));
        }

        private async Task CheckOut()
        {

            string chekoutUrl = await CarroService.Checkout(Convert.ToInt32(userId));
            Navigation.NavigateTo(chekoutUrl);

        }
        



    }
}