using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Command.RoomType;
using HotelManagement.Application.Dtos.Request;
using HotelManagement.Application.Query.RoomType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomCategoryController : BaseController
    {
        private readonly IMediator _mediator;

        public RoomCategoryController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllRoomType")]
        public async Task<IActionResult> GetAllRoomType( ) 
        => HandleResponse(await Mediator.Send(new GetAllRoomTypeQuery()));

        [HttpPost]
        [Route("CreateRoomType")]
        public async Task<IActionResult> CreateRoomType( RoomTypeRequestDto requestDto) 
        => HandleResponse(await Mediator.Send(new CreateRoomTypeCommand(requestDto)));

        [HttpDelete]
        [Route("DeleteRoomType")]
        public async Task<IActionResult> DeleteRoomType( string typeName) 
        => HandleResponse(await Mediator.Send(new DeleteRoomTypeCommand(typeName)));

        [HttpPut]
        [Route("UpdateRoomType")]
        public async Task<IActionResult> UpdateRoomType( RoomTypeRequestDto requestDto) 
        => HandleResponse(await Mediator.Send(new UpdateRoomTypeCommand(requestDto)));
    }
}