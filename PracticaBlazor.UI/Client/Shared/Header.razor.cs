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
using System.Security.Claims;
using PracticaBlazor.UI.Shared.Models;

namespace PracticaBlazor.UI.Client.Shared
{
    public partial class Header : IDisposable
    {
        private string searchContent = "";
        private int totalCarrito = 0;
        private Producto selectedProduct;
        public int TotalCarrito
        {
            get => totalCarrito;
            set
            {
                totalCarrito = value;
                InvokeAsync(() => StateHasChanged());
            }
        }
        AuthenticationState authState;
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            authState = await authenticationStateTask;
            CarroService.CarritoChanged += CarroService_CarritoChanged;
            totalCarrito = await CarroService.ContadorCarrito(authState);
        }

        private void CarroService_CarritoChanged()
        {
            this.StateHasChanged();
        }

        void Carrito()
        {
            Navigation.NavigateTo("/micarrito");
        }

        void Search()
        {
            Navigation.NavigateTo($"/productos/{searchContent}");
        }
        private void OnInputEvent(ChangeEventArgs changeEvent)
        {
            searchContent = (string)changeEvent.Value;
        }

        private bool _disposed = false;
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            CarroService.CarritoChanged -= CarroService_CarritoChanged;
        }

        //Buscador
        private void HandleSearch(Producto product)
        {
            if (product == null) return;
            selectedProduct = product;
            Navigation.NavigateTo($"/producto/ver/{selectedProduct.Id}");
        }

        private async Task<IEnumerable<Producto>> SearchProduct(string searchText)
        {
            var response = await ProductoService.SearchProducto(searchText);
            return response;
        }
    }
}