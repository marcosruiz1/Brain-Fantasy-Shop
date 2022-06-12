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
using PracticaBlazor.UI.Shared.Models.Dto.Usuario;

namespace PracticaBlazor.UI.Client.Pages.Usuarios
{
    public partial class CrearUser
    {
        private Usuario _usuarioFinal = new();
        private UsuarioRegistroAdminDto _usuario = new();
        private string Rol { get; set; }
        private List<string> _roles = new() { "ROLE_VISUALIZAR", "ROLE_COMPLETO", "ROLE_ADMIN", "ROLE_VIP" };

        private async Task Post()
        {
            if (_usuario.Imagen != null)
            {
                _usuarioFinal.Username = _usuario.Username;
                _usuarioFinal.Email = _usuario.Email;
                _usuarioFinal.Telefono = _usuario.Telefono;
                _usuarioFinal.Direccion = _usuario.Direccion;
                _usuarioFinal.Nombre = _usuario.Nombre;
                _usuarioFinal.Apellidos = _usuario.Apellidos;
                _usuarioFinal.Rol = _usuario.Rol;
                _usuarioFinal.Imagen = _usuario.Imagen;
                _usuarioFinal.Password = _usuario.Password;
                _usuarioFinal.ConfirmPassword = _usuario.Password;
                await Http.PostAsJsonAsync($"api/Usuarios/", _usuarioFinal);
                Navigation.NavigateTo("/usuarios/admin");
                toastService.ShowSuccess("Usuario añadido con éxito");
            }
            else
            {
                toastService.ShowError("Selecciona una imagen");
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
            _usuario.Imagen = $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
        }
    }
}