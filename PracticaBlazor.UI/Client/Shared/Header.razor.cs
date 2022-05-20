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
using System.Security.Claims;
using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Client.Shared
{
    public partial class Header
    {
        private int totalCarrito = 0;
        private List<Carrito> _carritosUser = new();

        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        private string userId;

        protected override async Task OnInitializedAsync()
        {

            //GET user Id
            var authState = await authenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (userId != null)
                {
                    _carritosUser = await Http.GetFromJsonAsync<List<Carrito>>($"/api/Carritos/User/{userId}");
                }
                foreach (var carrito in _carritosUser)
                {
                    totalCarrito += carrito.numProductos;
                }
            }
            

        }
        void Carrito()
        {
            Navigation.NavigateTo("/micarrito");
        }
    }
}