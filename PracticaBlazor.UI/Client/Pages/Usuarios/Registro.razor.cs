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

namespace PracticaBlazor.UI.Client.Pages.Usuarios
{
    public partial class Registro
    {
        private Usuario _usuario = new();
        protected override async Task OnInitializedAsync()
        {
            _usuario.Direccion = "";
        }

        private async Task Post()
        {
            await Http.PostAsJsonAsync("/api/Usuarios/", _usuario);
            Navigation.NavigateTo("/login");
        }
    }
}