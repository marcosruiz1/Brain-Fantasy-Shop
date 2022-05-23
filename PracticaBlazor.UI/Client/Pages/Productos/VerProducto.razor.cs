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
using PracticaBlazor.UI.Shared.Models.Dto;
using System.Security.Claims;
using PracticaBlazor.UI.Shared.Models.Dto.Producto;
using PracticaBlazor.UI.Shared.Models;


namespace PracticaBlazor.UI.Client.Pages.Productos
{
    public partial class VerProducto
    {
        [Parameter]
        public int Id { get; set; }

        private Producto _producto = new();
        private List<Categoria> _categorias = new();
        private ProductoCarritoDto _productoCarrito = new();
       

        //User
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        private string userId = "";
        private int totalProductos = 0;
        private List<Carrito> _carritos = new();
        private Carrito _carritoActual = new();
        private bool _comprobador = false;
        AuthenticationState authState;

        [CascadingParameter]
        public Header Layout { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _producto = await Http.GetFromJsonAsync<Producto>($"/api/Productos/{Id}");
            //GET categorías
            _categorias = await Http.GetFromJsonAsync<List<Categoria>>("/api/Categorias");
            //GET user Id
            authState = await authenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        public async Task CombrobarCarrito()
        {
            if(userId == null)
            {
                Navigation.NavigateTo("/login");
            }
            else
            {
                await CarroService.AgregarCarrito(Id, authState);
            }
            
        }
    }
}