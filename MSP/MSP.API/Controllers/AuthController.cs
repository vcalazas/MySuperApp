using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MSP.API.Services;
using MSP.Domain.DTOs;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserDto request){
            var result = await _jwtService.Authenticate(request, "");
            if (result is null)
                return Unauthorized();

            return result;
        }

        [AllowAnonymous]
        [HttpPost("Update")]
        public async Task<ActionResult<UserDto>> Update(UserDto request)
        {
            string? authorizationHeaderValue = Request.Headers["Authorization"];

            var result = await _jwtService.GenerateRefreshToken(authorizationHeaderValue, request);
            if (result is null)
                return Unauthorized();

            return result;
        }
    }
}
