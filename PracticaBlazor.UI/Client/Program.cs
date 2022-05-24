using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PracticaBlazor.UI.Client;
using Blazored.LocalStorage;
using Blazored.Toast;
using PracticaBlazor.UI.Shared.Models.Carro;
using PracticaBlazor.UI.Client.Shared;
using PracticaBlazor.UI.Shared.Models.Services.ProductoService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddScoped<ICarroService, CarroService>();
builder.Services.AddScoped<Header>();
builder.Services.AddScoped<IProductoService, ProductoService>();


await builder.Build().RunAsync();
