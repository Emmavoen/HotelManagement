using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Command.Booking;
using HotelManagement.Application.Dtos.Request;
using HotelManagement.Application.Query.Booking;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : BaseController
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("CheckBookingAvailability")]
        public async Task<IActionResult> CheckBookingAvailability(List<AvailableRooms> availableRooms) 
        => HandleResponse(await Mediator.Send(new CheckBookingAvailabilityQuery(availableRooms)));

        [HttpPut]
        [Route("CheckBookingAvailability")]
        public async Task<IActionResult> CreateBooking(BookingRequestDto requestDto) 
        => HandleResponse(await Mediator.Send(new CreateBookingCommand(requestDto)));
    }
}