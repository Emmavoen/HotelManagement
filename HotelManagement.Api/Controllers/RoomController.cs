using System.Threading.Tasks;
using HotelManagement.Application.Command.Room;
using HotelManagement.Application.Query.Room;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : BaseController
    {
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("add-room")]
        public async Task<IActionResult> AddRoom(CreateRoomRequestDto requestDto) 
        => HandleResponse(await Mediator.Send(new CreateRoomCommand(requestDto)));

        [HttpPut]
        [Route("update-room")]
        public async Task<IActionResult> UpdateRoom(UpdateRoomRequestDto requestDto) 
        => HandleResponse(await Mediator.Send(new UpdateRoomCommand(requestDto)));

        [HttpDelete]
        [Route("delete-room")]
        public async Task<IActionResult> DeleteRoom(int id) 
        => HandleResponse(await Mediator.Send(new DeleteRoomCommand(id)));

        [HttpPut]
        [Route("update-room-status")]
        public async Task<IActionResult> UpdateRoomStatus(ChangeRoomStatusRequestDto requestDto) 
        => HandleResponse(await Mediator.Send(new ChangeRoomStatusCommand(requestDto)));

        [HttpGet]
        [Route("get-available-rooms")]
        public async Task<IActionResult> GetAvailableRooms() 
        => HandleResponse(await Mediator.Send(new GetAvailableRoomsCommand()));

        [HttpGet]
        [Route("get-all-rooms")]
        public async Task<IActionResult> GetAllRooms() 
        => HandleResponse(await Mediator.Send(new GetAllRoomsCommand()));

        [HttpGet]
        [Route("get-room-by-id")]
        public async Task<IActionResult> GetRoomById(int id) 
        => HandleResponse(await Mediator.Send(new GetRoomByIdCommand(id)));
    }
}