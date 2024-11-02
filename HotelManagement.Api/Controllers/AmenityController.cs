using System.Threading.Tasks;
using HotelManagement.Application.Command.Amenity;
using HotelManagement.Application.Query.Amenity;
using HotelManagement.Application.Query.RoomAmenity;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static HotelManagement.Application.Command.Amenity.CreateAmenityHandler;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AmenityController : BaseController
    {
        private readonly IMediator _mediator;

        public AmenityController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-amenity")]
        public async Task<IActionResult> RequestRefund(CreateAmenityRequestDto requestDto )
        => HandleResponse(await _mediator.Send(new CreateAmenity(requestDto)));

        [HttpPut("update-amenity")]
        public async Task<IActionResult> UpdateAmenity(int id, UpdateAmenityRequestDto requestDto )
        => HandleResponse(await _mediator.Send(new UpdateAmenity(id,requestDto)));

        [HttpGet("get-all-amenities")]
        public async Task<IActionResult> GetAllAmenities()
        => HandleResponse(await _mediator.Send(new GetAmenities()));

        [HttpDelete("delete-amenity")]
        public async Task<IActionResult> DeleteAmenity(int id)
        => HandleResponse(await _mediator.Send(new DeleteAmenity(id)));

        [HttpPut("activate-deactivate-amenity")]
        public async Task<IActionResult> ActivateDeactivateAmenity(ActivateDeactivateAmenityRequestDto requestDto)
        => HandleResponse(await _mediator.Send(new ActivateDeactivateAmenity(requestDto.AmenityId, requestDto.IsActive)));

        [HttpPost("assign-amenity-to-room-amenity")]
        public async Task<IActionResult> AssignAmenityToRoom(int roomAmenitiesId, int amenityId)
        => HandleResponse(await _mediator.Send(new AssignAmenityToRoomCommand(roomAmenitiesId, amenityId)));
    }
}