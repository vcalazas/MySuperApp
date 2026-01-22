using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSP.Domain.Business;
using MSP.Domain.DTOs;
using MSP.Domain.Entities;

namespace MSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemSettingsController : ControllerBase
    {
        private readonly ISystemSettingsBusiness _systemSettingsBusiness;

        public SystemSettingsController(ISystemSettingsBusiness systemSettingsBusiness)
        {
            _systemSettingsBusiness = systemSettingsBusiness;
        }

        //[Authorize]
        [HttpGet]
        //[SwaggerOperation(
        //    Summary = "Creates a new product",
        //    Description = "Requires admin privileges",
        //    OperationId = "CreateProduct",
        //    Tags = ["Purchase", "Products"]
        //)]
        //[Obsolete("This endpoint is deprecated. Use the new endpoint instead.")]
        public async Task<ActionResult<IEnumerable<MSPSystemSettingsDTO>>>  Get()
        {
            try
            {
                return StatusCode(200, await _systemSettingsBusiness.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
