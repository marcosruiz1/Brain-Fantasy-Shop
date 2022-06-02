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
        AuthenticationState authState;
        private string userId = "";
        //User
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        protected override async Task OnInitializedAsync()
        {
            _productos = await Http.GetFromJsonAsync<List<Producto>>("/api/Productos");
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
    }
}