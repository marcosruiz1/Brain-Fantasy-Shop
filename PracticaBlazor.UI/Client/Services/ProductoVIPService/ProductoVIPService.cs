﻿using PracticaBlazor.UI.Shared.Models;
using System.Net.Http.Json;

namespace PracticaBlazor.UI.Client.Services.ProductoVIPService
{
    public class ProductoVIPService : IProductoVIPService
    {
        HttpClient Http;

        public ProductoVIPService(HttpClient Http)
        {
            this.Http = Http;
        }

        public Task ActualizarProducto(ProductoVIPs productoVIP)
        {
            throw new NotImplementedException();
        }

        public async Task AgregarProductoVIP(ProductoVIPs productoVIP)
        {
            await Http.PostAsJsonAsync($"/api/ProductosVIPs/", productoVIP);
        }

        public Task BorrarProductoVIP(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductoVIPs>> ProductosVIP()
        {
            return await Http.GetFromJsonAsync<List<ProductoVIPs>>($"/api/ProductosVIPs/");
        }

        public async Task<List<ProductoVIPs>> ProductosVIPAdmitidos()
        {
            return await Http.GetFromJsonAsync<List<ProductoVIPs>>($"/api/ProductosVIPs/admitidos");
        }

        public async Task<List<ProductoVIPs>> ProductosVIPDenegados()
        {
            return await Http.GetFromJsonAsync<List<ProductoVIPs>>($"/api/ProductosVIPs/denegados");
        }

        public async Task<List<ProductoVIPs>> ProductosVIPEspera()
        {
            return await Http.GetFromJsonAsync<List<ProductoVIPs>>($"/api/ProductosVIPs/espera");
        }
    }
}
