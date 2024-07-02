using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Primitives;
using MultaqaTech.APIs.Controllers;
using Stripe.Checkout;

namespace Server.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class CheckoutController(IConfiguration configuration, IBasketRepository basketRepository) : BaseApiController
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IBasketRepository _basketRepository = basketRepository;
    private static string _clientURL = string.Empty;

    [HttpPost]
    public async Task<ActionResult> CheckoutOrder([FromServices] IServiceProvider sp)
    {
        StringValues referer = Request.Headers.Referer;
        _clientURL = referer[0];

        // Build the URL to which the customer will be redirected after paying.
        var server = sp.GetRequiredService<Microsoft.AspNetCore.Hosting.Server.IServer>();

        var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

        string? thisApiUrl = null;

        if (serverAddressesFeature is not null)
        {
            thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
        }

        if (thisApiUrl is null)
            return StatusCode(500);

        var sessionId = await CheckOut(thisApiUrl);
        string PublishableKey = _configuration["Stripe:PublishableKey"] ?? string.Empty;

        var checkoutOrderResponse = new CheckoutOrderResponse()
        {
            SessionId = sessionId,
            PublishableKey = PublishableKey
        };

        return Ok(checkoutOrderResponse);
    }

    [NonAction]
    public async Task<string> CheckOut(string thisApiUrl)
    {
        // Create a payment flow from the items in the cart.
        // Gets sent to Stripe API.
        string email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

        StudentBasket? basket = await _basketRepository.GetBasket(email);

        List<SessionLineItemOptions>? lineItems = [];

        basket?.BasketItems?.ForEach(basketItem => lineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmountDecimal = basketItem.Price,
                Currency = "egp",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = basketItem.CourseTitle,
                    Images = [basketItem.CourseThumbnailUrl]
                }
            },
            Quantity = 1,
        }));

        var options = new SessionCreateOptions
        {
            // Stripe calls the URLs below when certain checkout events happen such as success and failure.
            SuccessUrl = $"{thisApiUrl}/checkout/success?sessionId=" + "{CHECKOUT_SESSION_ID}", // Customer paid.
            CancelUrl = _clientURL + "failed",  // Checkout cancelled.
            PaymentMethodTypes =
            [
                "card"
            ],
            LineItems = lineItems,

            Mode = "payment" // One-time payment. Stripe supports recurring 'subscription' payments.
        };

        var service = new SessionService();
        var session = await service.CreateAsync(options);

        return session.Id;
    }

    [HttpGet("success")]
    // Automatic query parameter handling from ASP.NET.
    // Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
    public ActionResult CheckoutSuccess([FromQuery] string sessionId)
    {
        var sessionService = new SessionService();
        var session = sessionService.Get(sessionId);

        // Here you can save order and customer details to your database.
        var total = session.AmountTotal.Value;
        var customerEmail = session.CustomerDetails.Email;

        return Redirect(_clientURL + "success");
    }
}