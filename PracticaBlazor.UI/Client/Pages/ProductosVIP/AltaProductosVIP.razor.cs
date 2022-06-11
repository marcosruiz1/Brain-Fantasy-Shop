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
using PracticaBlazor.UI.Shared.Models.Dto.ProductoVIP;

namespace PracticaBlazor.UI.Client.Pages.ProductosVIP
{
    public partial class AltaProductosVIP
    {
        private List<ProductoVIPs> _productosEspera;
        private List<ProductoVIPs> _productoAdmitido;
        private List<ProductoVIPs> _productoDenegado;
        private List<Categoria> _categorias;
        private Producto _producto = new();
        private ProductoVIPAltaDto _productoAlta = new();


        //USER
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        AuthenticationState authState;
        private string userId;
        


        protected override async Task OnInitializedAsync()
        {
            //GET user Id
            authState = await authenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                await GetProductosVIP();
            }           
            _categorias = await CategoriaService.GetCategoriaas();


        }

        public async Task Aceptar(EditContext formContext, ProductoVIPs productoVIP)
        {
            ComprobarButton(formContext);
            _producto.Descripcion = productoVIP.Descripcion;
            _producto.Nombre = productoVIP.Nombre;
            _producto.Imagen = productoVIP.Imagen;
            _producto.IsVIP = true;
            _producto.Precio = _productoAlta.PrecioFinal;
            await ProductoService.CrearProducto(_producto);
            productoVIP.Estado = "ADMITIDO";
            productoVIP.Categoria = _producto.Categoria;
            productoVIP.PrecioFinal = _producto.Precio;
            await ProductoVIPService.ActualizarProducto(productoVIP);
            await GetProductosVIP();

        }

        public async Task Denegar(EditContext formContext, ProductoVIPs productoVIP)
        {
            ComprobarButton(formContext);
            productoVIP.Estado = "DENEGADO";
            await ProductoVIPService.ActualizarProducto(productoVIP);
            await GetProductosVIP();

        }

        private void ComprobarButton(EditContext formContext)
        {
            bool formIsValid = formContext.Validate();
            if (formIsValid == false)
                return;
   
        }

        private async Task GetProductosVIP()
        {
            _productosEspera = await ProductoVIPService.ProductosVIPEspera();
            _productoAdmitido = await ProductoVIPService.ProductosVIPAdmitidos();
            _productoDenegado = await ProductoVIPService.ProductosVIPDenegados();
        }
    }
}