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
using PracticaBlazor.UI.Shared.Models.Dto;
using System.Security.Claims;
using PracticaBlazor.UI.Shared.Models.Dto.Producto;
using PracticaBlazor.UI.Shared.Models;


namespace PracticaBlazor.UI.Client.Pages.Productos
{
    public partial class VerProducto
    {
        [Parameter]
        public int Id { get; set; }

        private Producto _producto;
        
        private ProductoCarritoDto _productoCarrito = new();
       

        //User
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        private string userId = "";
        AuthenticationState authState;
        
        //Comentario 
        private Comentario _comentario = new();
        private List<Comentario> _comentarios = new();
        private List<Usuario> _usuarios = new();
        private bool _comprobarComentario = false;

        //Categoria
        private string _categoria;
        private List<Categoria> _categorias = new();
        protected override async Task OnParametersSetAsync()
        {
            //GET user Id
            authState = await authenticationStateTask;
            if (authState.User.Identity.IsAuthenticated)
            {
                userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }

            //GET Producto
            _producto = await Http.GetFromJsonAsync<Producto>($"/api/Productos/{Id}");

            //GET comentarios
            _comentarios = await ComentarioService.ComentariosProducto(Id);
            _usuarios = await ComentarioService.ComentariosUser(Id);
            _comprobarComentario = await ComentarioService.ComprobarComentario(Convert.ToInt32(userId), Id);

            //GET categorías
            _categorias = await Http.GetFromJsonAsync<List<Categoria>>("/api/Categorias");
            _categoria = await CategoriaService.GetCategoriaNombre(_producto.Categoria);

            
        }

        public async Task CombrobarCarrito()
        {
            if(authState.User.Identity.IsAuthenticated)
            {              
                await CarroService.AgregarCarrito(Id, authState);
            }
            else
            {
                Navigation.NavigateTo("/login");
            }
            
        }

        private async Task CrearComentario()
        {
            if (authState.User.Identity.IsAuthenticated)
            {
                _comentario.fecha = DateTime.Now;
                _comentario.idProducto = Id;
                _comentario.idUsuario = Convert.ToInt32(userId);
                await ComentarioService.AgregarComentario(_comentario);
                toastService.ShowSuccess("Comentario añadido");
                _comentarios =  await ComentarioService.ComentariosProducto(Id);
                _usuarios = await ComentarioService.ComentariosUser(Id);
                _comentario.Mensaje = "";
                _comprobarComentario = await ComentarioService.ComprobarComentario(Convert.ToInt32(userId), Id);
                StateHasChanged();
            }
            else
            {
                Navigation.NavigateTo("/login");
            }
            
        }
    }
}