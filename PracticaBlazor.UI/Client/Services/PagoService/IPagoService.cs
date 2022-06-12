using PracticaBlazor.UI.Shared.Models;
using PracticaBlazor.UI.Shared.Models.Dto.Producto;
using Stripe.Checkout;

namespace PracticaBlazor.UI.Client.Services.PagoService
{
    public interface IPagoService
    {
        Session CreateCheckoutSession(List<ProductoCarritoDto> productosCarro);
    }
}
