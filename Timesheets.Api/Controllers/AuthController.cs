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
        public async Task<IActionResult> Authenticate([FromQuery] string user, string password)
        {
            var resultAuth = await _userAuthenticate.Authenticate(user, password);

            if (resultAuth is null)
                return BadRequest(new { message = "Username or password is incorrect" });

            HttpContext.Response.Cookies.Append("refresh_token", resultAuth.RefreshToken);

            return Ok(resultAuth.AccessToken);
        }


        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshTokens()
        {
            string refreshToken = HttpContext.Request.Cookies["refresh_token"];

            var resultAuth = await _userAuthenticate.GetNewPairToken(refreshToken);

            if (resultAuth is null)
                return BadRequest(new { message = "refresh_token is incorrect" });

            return Ok(resultAuth.AccessToken);
        }
    }
}
