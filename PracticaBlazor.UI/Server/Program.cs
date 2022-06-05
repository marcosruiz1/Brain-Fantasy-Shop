using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using PracticaBlazor.UI.Server.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using PracticaBlazor.UI.Shared.Models.Email;
using PracticaBlazor.UI.Shared.Models;
using PracticaBlazor.UI.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticaBlazor.UI.Client.Services.CarroService;
using PracticaBlazor.UI.Client.Services.ProductoService;
using PracticaBlazor.UI.Client.Services.CategoriaService;
using PracticaBlazor.UI.Client.Services.ComentarioService;
using PracticaBlazor.UI.Client.Services.UsuarioService;
using PracticaBlazor.UI.Client.Services.ProductoVIPService;

var builder = WebApplication.CreateBuilder(args);
var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddRazorPages();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:Token"]))
                 });
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddHttpClient<ICarroService, CarroService>();
builder.Services.AddHttpClient<IProductoService, ProductoService>();
builder.Services.AddHttpClient<IComentarioService, ComentarioService>();
builder.Services.AddHttpClient<ICategoriaService, CategoriaService>();
builder.Services.AddHttpClient<IUsuarioService, UsuarioService>();
builder.Services.AddHttpClient<IProductoService, ProductoService>();
builder.Services.AddHttpClient<IProductoVIPService, ProductoVIPService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor API V1");
});

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
