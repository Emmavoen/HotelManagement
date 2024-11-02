using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Command.RoomAmenity;
using HotelManagement.Application.Query.RoomAmenity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomAmenityController : BaseController
    {
        private readonly IMediator _mediator;

        public RoomAmenityController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-room-amenity")]
        public async Task<IActionResult> AddRoomAmenity()
        => HandleResponse(await _mediator.Send(new CreateRoomAmenityCommand()));

        [HttpPost("get-amenities-by-roomamenityid")]
        public async Task<IActionResult> GetAmenitiesByRoomAmenityId(int roomAmenityId)
        => HandleResponse(await _mediator.Send(new GetAmenitiesByRoomAmenitiesId(roomAmenityId)));
    }
}