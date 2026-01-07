using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSP.API.Model;
using MSP.API.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Auth : ControllerBase
    {
        private readonly JwtService _jwtService;

        public Auth(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseAPIModel>> Login(LoginRequestAPIModel request){
            var result = await _jwtService.Authenticate(request);
            if (result is null)
                return Unauthorized();

            return result;
        }
        
    }
}
