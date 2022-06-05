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

namespace PracticaBlazor.UI.Client.Pages.ProductosVIP
{
    public partial class AltaProductosVIP
    {
        private List<ProductoVIPs> _productosEspera;
        private List<ProductoVIPs> _productoAdmitido;
        private List<ProductoVIPs> _productoDenegado;
        private List<Categoria> _categorias;
        private Producto _producto = new();

        protected override async Task OnParametersSetAsync()
        {
            _productosEspera = await ProductoVIPService.ProductosVIPEspera();
            _productoAdmitido = await ProductoVIPService.ProductosVIPAdmitidos();
            _productoDenegado = await ProductoVIPService.ProductosVIPDenegados();
            _categorias = await CategoriaService.GetCategoriaas();


        }

        public async Task Aceptar(EditContext formContext)
        {
            ComprobarButton(formContext);

        }

        public async Task Denegar(EditContext formContext)
        {
            ComprobarButton(formContext);

        }

        private void ComprobarButton(EditContext formContext)
        {
            bool formIsValid = formContext.Validate();
            if (formIsValid == false)
                return;
   
        }
    }
}