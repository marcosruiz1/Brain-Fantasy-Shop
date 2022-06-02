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
using PracticaBlazor.UI.Shared.Models.Dto;

namespace PracticaBlazor.UI.Client.Pages.Productos
{
    public partial class CreateProduct
    {
        private Producto _producto = new();
        private List<Categoria> _categorias = new();
        private async Task Post()
        {
            if(_producto.Imagen != null)
            {
                await ProductoService.CrearProducto(_producto);
                Navigation.NavigateTo("/productos");
            }
            else
            {
                toastService.ShowError("Selecciona una imagen");
            }
            
        }

        //Conseguir las categorías
        protected override async Task OnInitializedAsync()
        {
            _categorias = await Http.GetFromJsonAsync<List<Categoria>>("/api/Categorias");
        }

        private async Task OnFileChange(InputFileChangeEventArgs ev)
        {
            //get the file
            var file = ev.File;
            //read that file in a byte array
            var buffer = new byte[file.Size];
            await file.OpenReadStream(1512000).ReadAsync(buffer);
            //convert byte array to base 64 string
            _producto.Imagen = $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
        }
    }
}