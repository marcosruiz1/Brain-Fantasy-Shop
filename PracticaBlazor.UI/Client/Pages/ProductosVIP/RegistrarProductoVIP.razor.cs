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
    public partial class RegistrarProductoVIP
    {
        private ProductoVIPs _productoVIPs = new();

        //User
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        AuthenticationState authState;
        private string userId = "";


        protected override async Task OnInitializedAsync()
        {
            authState = await authenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }
        public async Task Post()
        {
            if (authState.User.Identity.IsAuthenticated)
            {
                _productoVIPs.IdUsuario = Convert.ToInt32(userId);
                await ProductoVIPService.AgregarProductoVIP(_productoVIPs);
                Navigation.NavigateTo("/productosVIP/usuario");
                toastService.ShowSuccess("Produto registrado exitosamente");
            }

        }
        private async Task OnFileChange(InputFileChangeEventArgs ev)
        {
            //get the file
            var file = ev.File;
            //read that file in a byte array
            var buffer = new byte[file.Size];
            await file.OpenReadStream(1512000).ReadAsync(buffer);
            //convert byte array to base 64 string
            _productoVIPs.Imagen = $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
        }
    }
 
}