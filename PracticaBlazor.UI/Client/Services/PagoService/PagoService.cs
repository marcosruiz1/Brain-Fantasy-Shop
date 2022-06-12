using PracticaBlazor.UI.Shared.Models;
using PracticaBlazor.UI.Shared.Models.Dto.Producto;
using Stripe;
using Stripe.Checkout;

namespace PracticaBlazor.UI.Client.Services.PagoService
{
    public class PagoService : IPagoService
    {
        public PagoService()
        {
            StripeConfiguration.ApiKey = "sk_test_51L9oAjBx8Vjl9FYvkn0va0nYEPgcoVflmOMzj2PkWPAX79HTvJQLgRGkiAxNIEBI7jBTwkSPd3XJayJvwZp5jHMJ00y5Pf9yGr";
        }

        public Session CreateCheckoutSession(List<ProductoCarritoDto> productosCarro)
        {
            var lineItems = new List<SessionLineItemOptions>();
            productosCarro.ForEach(pc => lineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = pc.Precio *100,
                    Currency = "eur",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = pc.Nombre,
                        
                    }
                },
                Quantity = pc.Cantidad
            }));
            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "https://localhost:7026/order-success",
                CancelUrl = "https://localhost:7026/micarrito",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }


    }
}
