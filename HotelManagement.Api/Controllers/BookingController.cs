using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Command.Booking;
using HotelManagement.Application.Dtos.Request;
using HotelManagement.Application.Query.Booking;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;

        public BookingController(IMediator mediator, ILogger logger) : base(mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("CheckBookingAvailability")]
        public async Task<IActionResult> CheckBookingAvailability(List<AvailableRooms> availableRooms) 
        => HandleResponse(await Mediator.Send(new CheckBookingAvailabilityQuery(availableRooms)));

        [HttpPut]
        [Route("CreateBooking")]
        public async Task<IActionResult> CreateBooking(BookingRequestDto requestDto) 
        {
        
            _logger.Information("something about serilog.......");
            return HandleResponse(await Mediator.Send(new CreateBookingCommand(requestDto)));
        }
    }
}