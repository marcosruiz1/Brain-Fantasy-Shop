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

namespace PracticaBlazor.UI.Client.Pages.Productos
{
    public partial class IndexProductAdmin
    {
        private List<Producto>? _productos;
        public string Filter { get; set; }
        public int Index { get; set; } = 0;

        protected override async Task OnInitializedAsync()
        {
            _productos = await ProductoService.GetProductos();
        }

        private async Task Delete(int id)
        {
            await CarroService.BorrarCarritoProd(id);
            await ComentarioService.BorrarComentarioProducto(id);
            await ComentarioService.BorrarComentarioProducto(id);
            await ProductoService.DeleteProducto(id);
            _productos = await ProductoService.GetProductos();
            toastService.ShowSuccess("Producto eliminado");
            StateHasChanged();
        }

        private void Edit(int id)
        {
            Navigation.NavigateTo($"/producto/edit/{id}");
        }

        private void Create()
        {
            Navigation.NavigateTo("/producto/create");
        }

        public bool IsVisible(Producto producto)
        {
            if (string.IsNullOrEmpty(Filter))
                return true;
            if (producto.Nombre.Contains(Filter, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}