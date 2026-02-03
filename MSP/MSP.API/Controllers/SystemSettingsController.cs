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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MSPSystemSettingsDTO>>>  Get()
        {
            try
            {
                return StatusCode(200, await _systemSettingsBusiness.GetAllAsync(false));
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("enabled")]
        public async Task<ActionResult<IEnumerable<MSPSystemSettingsDTO>>> GetEnabled()
        {
            try
            {
                return StatusCode(200, await _systemSettingsBusiness.GetAllAsync(true));
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MSPSystemSettingsDTO>> Post([FromBody] MSPSystemSettingsDTO mSPSystemSettings)
        {
            try
            {
                return StatusCode(200, await _systemSettingsBusiness.AddAsync(mSPSystemSettings));
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<MSPSystemSettingsDTO>> Put([FromBody] MSPSystemSettingsDTO mSPSystemSettings)
        {
            try
            {
                return StatusCode(200, await _systemSettingsBusiness.UpdateAsync(mSPSystemSettings));
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
