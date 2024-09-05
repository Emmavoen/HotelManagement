using System.Threading.Tasks;
using HotelManagement.Infrastructure.ExternalServiceImplementation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : BaseController
    {
        private readonly IMediator _mediator;

        public PaymentController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreatePayment")]
        public async Task<IActionResult> CreatePayment(MockPaymentRequest request)
        => HandleResponse(await _mediator.Send(new MockPaymentQuery(request)));

        // public async Task<IActionResult> CreatePayment(decimal amount)
        // {
        //     var payment =  await _mediator.Send(new CreatePaymentQuery(amount));
        //     return Ok(payment);
        // }
    }
}