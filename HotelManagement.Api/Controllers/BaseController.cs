using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Application.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator Mediator;
    public BaseController( IMediator mediator)
    {
        Mediator = mediator;
    }

    internal IActionResult  HandleResponse<T>(Result<T> result)
    {
        return result.statusCode switch
        {
            System.Net.HttpStatusCode.OK => Ok(result),
            System.Net.HttpStatusCode.BadRequest => BadRequest(result),
            System.Net.HttpStatusCode.Unauthorized => Unauthorized(result),
            System.Net.HttpStatusCode.NotFound => NotFound(result),
            System.Net.HttpStatusCode.Conflict => Conflict(result),
            _ => StatusCode(500, result)
        };
    }
    }
}