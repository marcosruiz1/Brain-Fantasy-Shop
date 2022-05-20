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

namespace PracticaBlazor.UI.Client.Pages
{
    public partial class Index
    {
        private List<Producto> _productos;
        private List<Producto> _productosNew = new();
        protected override async Task OnInitializedAsync()
        {
            _productos = await Http.GetFromJsonAsync<List<Producto>>("/api/Productos");
            Console.WriteLine(_productos[1]);
            _productosNew.Add(_productos[_productos.Count-1]);
            _productosNew.Add(_productos[_productos.Count-2]);
            _productosNew.Add(_productos[_productos.Count-3]);
        }
    }
}