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

namespace PracticaBlazor.UI.Client.Pages.ProductosVIP
{
    public partial class ProductosVIPUsuario
    {
        private List<ProductoVIPs> _productosEspera;
        private List<ProductoVIPs> _productoAdmitido;
        private List<ProductoVIPs> _productoDenegado;

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
                _productosEspera = await ProductoVIPService.ProductosVIPEsperaUser(Convert.ToInt32(userId));
                _productoAdmitido = await ProductoVIPService.ProductosVIPAdmitidosUser(Convert.ToInt32(userId));
                _productoDenegado = await ProductoVIPService.ProductosVIPDenegadosUser(Convert.ToInt32(userId));
            }
            
        }
        public void CrearProductoVIP()
        {
            Navigation.NavigateTo("/productosVIP/registro");
        }
    }
}