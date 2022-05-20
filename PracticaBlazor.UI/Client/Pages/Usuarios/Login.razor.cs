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
using Microsoft.JSInterop;
using PracticaBlazor.UI.Client;
using PracticaBlazor.UI.Client.Shared;
using Blazored.LocalStorage;
using Blazored.Toast;
using Blazored.Toast.Services;
using PracticaBlazor.UI.Shared.Models;
using Microsoft.AspNetCore.Authorization;

namespace PracticaBlazor.UI.Client.Pages.Usuarios
{
    public partial class Login
    {
        private UsuarioDto _user = new UsuarioDto();
        async Task HandleLogin()
        {
            var result = await Http.PostAsJsonAsync<UsuarioDto>("/api/Auth/login", _user);
            var token = await result.Content.ReadAsStringAsync();
            Console.WriteLine(token);
            if (token != string.Empty)
            {
                await LocalStorage.SetItemAsync("token", token);
                await AuthStateProvider.GetAuthenticationStateAsync();
                Navigation.NavigateTo("/");
            }
            else
            {
                toastService.ShowError("Login incorrecto");
            }
        }

        public void ForgotPassword()
        {
            Navigation.NavigateTo("/Account/ForgotPassword");
        }
    }
}