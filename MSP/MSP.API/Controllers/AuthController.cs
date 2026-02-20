using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MSP.API.Services;
using MSP.Domain.Business;
using MSP.Domain.BusinessInterfaces;
using MSP.Domain.DTOs;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        private readonly IConfiguration _configuration;

        public AuthController( IAuthBusiness authBusiness, IConfiguration configuration)
        {
            _authBusiness = authBusiness;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<MSPAuthDTO>> Login(MSPAuthDTO request){
            /*var result = await _jwtService.Authenticate(request, "");
            if (result is null)
                return Unauthorized();

            return result;*/

            return await HttpCustomValidator.IsValidAsync<MSPAuthDTO>(async () => 
                await _authBusiness.LoginAsync(new MSPAuthDTO()
                    {
                        PersonLogin = request.PersonLogin,
                        PersonPassworld = request.PersonPassworld,
                        DeviceType = request.DeviceType,
                        SO = request.SO,
                        Manufacturer = request.Manufacturer,
                        Model = request.Model,
                        Version = request.Version,
                        JwtConfigIssuer = _configuration["JwtConfig:Issuer"],
                        JwtConfigAudience = _configuration["JwtConfig:Audience"],
                        JwtConfigIssuerKey = _configuration["JwtConfig:Key"],
                        JwtConfigIssuerTokenValidiyMins = _configuration.GetValue<int>("JwtConfig:TokenValidiyMins"),
                    })
            );
        }

        [AllowAnonymous]
        [HttpPut("Update")]
        public async Task<ActionResult<MSPAuthDTO>> Update(MSPAuthDTO request)
        {
            string? authorizationHeaderValue = Request.Headers["Authorization"];

            var result = await _authBusiness.UpdateAsync(authorizationHeaderValue, new MSPAuthDTO()
            {
                AuthId = request.AuthId,
                PersonLogin = request.PersonLogin,
                PersonPassworld = request.PersonPassworld,
                DeviceType = request.DeviceType,
                SO = request.SO,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                Version = request.Version,
                JwtConfigIssuer = _configuration["JwtConfig:Issuer"],
                JwtConfigAudience = _configuration["JwtConfig:Audience"],
                JwtConfigIssuerKey = _configuration["JwtConfig:Key"],
                JwtConfigIssuerTokenValidiyMins = _configuration.GetValue<int>("JwtConfig:TokenValidiyMins")
            });
            if (result is null)
                return Unauthorized();

            return result;
        }
    }
}
