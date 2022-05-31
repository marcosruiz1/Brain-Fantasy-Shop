using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PracticaBlazor.UI.Client;
using Blazored.LocalStorage;
using Blazored.Toast;
using PracticaBlazor.UI.Client.Shared;
using PracticaBlazor.UI.Client.Services.CarroService;
using PracticaBlazor.UI.Client.Services.ProductoService;
using PracticaBlazor.UI.Client.Services.ComentarioService;
using PracticaBlazor.UI.Client.Services.CategoriaService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped<ICarroService, CarroService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<IComentarioService, ComentarioService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();




await builder.Build().RunAsync();
