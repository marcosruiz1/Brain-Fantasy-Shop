using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticaBlazor.UI.Client.Services.PagoService;
using PracticaBlazor.UI.Shared.Models;
using PracticaBlazor.UI.Shared.Models.Dto.Producto;

namespace PracticaBlazor.UI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagoController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpPost("checkout")]
        public ActionResult CreateCheckoutSession(List<ProductoCarritoDto> productosCarrito)
        {
            var session = _pagoService.CreateCheckoutSession(productosCarrito);
            return Ok(session.Url);
        }
    }
}
