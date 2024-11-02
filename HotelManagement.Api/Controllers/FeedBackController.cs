using System.Threading.Tasks;
using HotelManagement.Application.Command.FeedBack;
using HotelManagement.Application.Query.FeedBack;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedBackController : BaseController
    {
        private readonly IMediator _mediator;

        public FeedBackController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-feedback")]
        public async Task<IActionResult> AddFeedback(CreateFeedbackRequestDto request)
        => HandleResponse(await _mediator.Send(new CreateFeedbackCommand(request)));

        [HttpPost("get-all-feedback")]
        public async Task<IActionResult> GetAllFeedback()
        => HandleResponse(await _mediator.Send(new GetFeedbackQuery()));
    }
}