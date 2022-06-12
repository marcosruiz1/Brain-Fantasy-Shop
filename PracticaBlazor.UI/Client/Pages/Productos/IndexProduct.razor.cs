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

namespace PracticaBlazor.UI.Client.Pages.Productos
{
    public partial class IndexProduct
    {
        [Parameter]
        public int Nombre { get; set; }
        private List<Producto> _productos;
        public string Filter { get; set; }
        public string FilterCategoria { get; set; }
        

        //User
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        AuthenticationState authState;
        private string userId = "";

        //Categorías
        private List<Categoria> _categorias = new();

        protected override async Task OnInitializedAsync()
        {
            _productos = await ProductoService.GetProductos();
            _categorias = await CategoriaService.GetCategoriaas();
            authState = await authenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }


        public void VerDetalle(int id)
        {
            Navigation.NavigateTo($"/producto/ver/{id}");
        }

        public async Task CombrobarCarrito(int id)
        {
            if (authState.User.Identity.IsAuthenticated)
            {
                await CarroService.AgregarCarrito(id, authState);
            }
            else
            {            
                Navigation.NavigateTo("/login");
            }

        }

        public bool IsVisible(Producto producto)
        {
            if (string.IsNullOrEmpty(Filter))
                return true;
            if (producto.Nombre.Contains(Filter, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
        public bool IsVisibleCategoria(Producto producto)
        {
            if (producto.Categoria == Convert.ToInt32(FilterCategoria))
                return true;
            if (Convert.ToInt32(FilterCategoria) == 0)
                return true;
            return false;
        }
    }
}