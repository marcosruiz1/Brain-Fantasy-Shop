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

namespace PracticaBlazor.UI.Client.Pages.Productos
{
    public partial class EditProduct
    {
        [Parameter]
        public int Id { get; set; }

        private Producto _producto;
        private List<Categoria> _categorias = new();

        protected override async Task OnInitializedAsync()
        {
            _producto = await Http.GetFromJsonAsync<Producto>($"/api/Productos/{Id}");
            //Conseguir las categorías
            _categorias = await Http.GetFromJsonAsync<List<Categoria>>("/api/Categorias");
        }

        private async Task Put()
        {
           // _producto.Categoria = Convert.ToInt32(CategoriaProducto);
            await Http.PutAsJsonAsync<Producto>($"/api/Productos/{_producto.Id}", _producto);
            Navigation.NavigateTo("/productos/admin");
            toastService.ShowSuccess($"Producto {_producto.Nombre} editado con éxito");
        }

        private void CambiarImagen(int id)
        {
            Navigation.NavigateTo($"/producto/edit/img/{id}");
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