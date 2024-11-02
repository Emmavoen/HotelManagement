using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Infrastructure.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : BaseController
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("send-booking-email")]
        public async Task<IActionResult> SendMail(EmailRequest request)
        => HandleResponse(await _mediator.Send(new SendEmailCommand(request)));
    }
}