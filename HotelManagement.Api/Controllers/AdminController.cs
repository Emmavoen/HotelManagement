using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    //[Authorize(Roles = "User")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,  Roles = "User")]
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAdmin()
        {
            return Ok("I am Admin");
        }
    }
}