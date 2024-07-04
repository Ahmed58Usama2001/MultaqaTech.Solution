﻿namespace Server.Controllers;

public class CheckoutController(IConfiguration configuration, IBasketRepository basketRepository, IOrderService orderService) : BaseApiController
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IBasketRepository _basketRepository = basketRepository;
    private readonly IOrderService _orderService = orderService;
    private string? _clientURL = string.Empty;

    [ProducesResponseType(typeof(CheckoutOrderResponse), StatusCodes.Status200OK)]
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
        string publishableKey = _configuration["Stripe:PublishableKey"] ?? string.Empty;

        CheckoutOrderResponse? checkoutOrderResponse = new()
        {
            SessionId = sessionId,
            PublishableKey = publishableKey
        };

        return Ok(checkoutOrderResponse);
    }

    [NonAction]
    public async Task<string> CheckOut(string thisApiUrl)
    {
        string email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
        StudentBasket? basket = await _basketRepository.GetBasket(email);

        // Create a payment flow from the items in the cart.
        // Gets sent to Stripe API.

        List<SessionLineItemOptions>? lineItems = [];

        basket?.BasketItems?.ForEach(basketItem => lineItems.Add(new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmountDecimal = basketItem.Price,
                Currency = "USD",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = basketItem.CourseTitle,
                    Images = [basketItem.CourseThumbnailUrl]
                }
            },
            Quantity = 1,
        }));

        SessionCreateOptions? options = new()
        {
            // Stripe calls the URLs below when certain checkout events happen such as success and failure.
            //SuccessUrl = $"{thisApiUrl}/checkout/success?sessionId=" + "{CHECKOUT_SESSION_ID}", // Customer paid.
            CancelUrl = _clientURL + "failed",  // Checkout cancelled.
            SuccessUrl = _clientURL + "success",  // Checkout cancelled.
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

    //[HttpGet("success")]
    //// Automatic query parameter handling from ASP.NET.
    //// Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
    //public async Task<ActionResult> CheckoutSuccess(string sessionId)
    //{
    //    var sessionService = new SessionService();
    //    var session = sessionService.Get(sessionId);

    //    // Here you can save order and customer details to your database.
    //    Order order = new() { UserEmail = session.CustomerDetails.Email };
    //    _ = await _orderService.CreateOrderAsync(order);

    //    return Redirect(_clientURL + "success");
    //}

    [HttpGet("success")]
    // Automatic query parameter handling from ASP.NET.
    // Example URL: https://localhost:7051/checkout/success?sessionId=si_123123123123
    public async Task<IActionResult> CheckoutSuccess()
    {
        // Here you can save order and customer details to your database.
        string email = User.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

        Order order = new() { UserEmail = email };
        _ = await _orderService.CreateOrderAsync(order);

        return Ok();
    }
}