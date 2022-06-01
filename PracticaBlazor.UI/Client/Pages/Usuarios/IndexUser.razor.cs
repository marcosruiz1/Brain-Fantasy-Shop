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
using PracticaBlazor.UI.Client.Services.ComentarioService;
using PracticaBlazor.UI.Shared.Models;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace PracticaBlazor.UI.Client.Pages.Usuarios
{
    public partial class IndexUser
    {
        private List<Usuario> _usuarios;
        public string Filter { get; set; }

        //User
        AuthenticationState authState;
        private string userId = "";
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _usuarios = await Http.GetFromJsonAsync<List<Usuario>>("/api/Usuarios");
            authState = await authenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }

        private async Task Delete(int id)
        {
            await Http.DeleteAsync($"/api/Carritos/user/{id}");
            await ComentarioService.BorrarComentarioUser(id);
            await Http.DeleteAsync($"/api/Usuarios/{id}");
            _usuarios = await Http.GetFromJsonAsync<List<Usuario>>("/api/Usuarios");
            toastService.ShowSuccess("Usuario eliminado");
            StateHasChanged();
        }

        private void Edit(int id)
        {
            Navigation.NavigateTo($"/usuario/edit/{id}");
        }

        private void Create()
        {
            Navigation.NavigateTo("/usuario/registro");
        }

        public bool IsVisible(Usuario usuario)
        {
            if (string.IsNullOrEmpty(Filter))
                return true;

            if (usuario.Username.Contains(Filter, StringComparison.OrdinalIgnoreCase))
                return true;


            return false;
        }
    }
}