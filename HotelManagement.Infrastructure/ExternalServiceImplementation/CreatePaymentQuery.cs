using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;

namespace HotelManagement.Infrastructure.ExternalServiceImplementation
{
    public class CreatePaymentQuery : IRequest<Order>
    {
        internal readonly decimal amount;
        internal readonly string currency;

        public CreatePaymentQuery(decimal amount, string currency = "USD")
        {
            this.amount = amount;
            this.currency = currency;
        }
    }

    public class CreatePaymentQueryHandler : IRequestHandler<CreatePaymentQuery, Order>
    {
        private readonly PayPalHttpClient _client;

        public CreatePaymentQueryHandler(IConfiguration configuration)
        {
            var environment = new SandboxEnvironment(
                configuration["PayPal:ClientId"],
                configuration["PayPal:ClientSecret"]);

            _client = new PayPalHttpClient(environment);
        }
        public async Task<Order> Handle(CreatePaymentQuery request, CancellationToken cancellationToken)
        {
            var orderRequest = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>
        {
            new PurchaseUnitRequest
            {
                AmountWithBreakdown = new AmountWithBreakdown
                {
                    CurrencyCode = request.currency,
                    Value = request.amount.ToString("F2")
                }
            }
        }
            };

            var req = new OrdersCreateRequest();
            req.Prefer("return=representation");
            req.RequestBody(orderRequest);

            var response = await _client.Execute(req);
            var order = response.Result<Order>();

            return order;
        }

        public async Task<Order> CapturePayment(string orderId)
        {
            var request = new OrdersCaptureRequest(orderId);
            request.Prefer("return=representation");
            var response = await _client.Execute(request);
            var order = response.Result<Order>();
            return order;
        }
    }
}