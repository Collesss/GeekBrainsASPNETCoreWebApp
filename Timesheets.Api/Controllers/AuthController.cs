using AuthenticateService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthenticate _userAuthenticate;

        public AuthController(IUserAuthenticate userAuthenticate)
        {
            _userAuthenticate = userAuthenticate;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Authenticate([FromQuery] string user, string password)
        {
            var resultAuth = _userAuthenticate.Authenticate(user, password);

            if (resultAuth is null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(resultAuth);
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult RefreshTokens(string refreshToken)
        {

            return BadRequest();
        }
    }
}
