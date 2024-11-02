using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Command.Refund;
using HotelManagement.Application.Query.Refund;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefundController : BaseController
    {
        private readonly IMediator _mediator;

        public RefundController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("request-refund")]
        public async Task<IActionResult> RequestRefund(CreateRefundRequestDto request)
        => HandleResponse(await _mediator.Send(new CreateRefundCommand(request)));

        [HttpGet("get-all-refund")]
        public async Task<IActionResult> GetAllRefund()
        => HandleResponse(await _mediator.Send(new GetRefundQuery()));
    }
}