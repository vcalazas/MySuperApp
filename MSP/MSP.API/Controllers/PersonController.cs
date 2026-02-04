using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSP.Domain.BusinessInterfaces;
using MSP.Domain.DTOs;
using MSP.Domain.Entities;

namespace MSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;

        public PersonController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MSPPersonDTO>>>  Get()
        {
            try
            {
                return StatusCode(200, await _personBusiness.GetAllAsync(false));
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet("enabled")]
        public async Task<ActionResult<IEnumerable<MSPPersonDTO>>> GetEnabled()
        {
            try
            {
                return StatusCode(200, await _personBusiness.GetAllAsync(true));
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<MSPPersonDTO>> Post(MSPPersonDTO MSPPerson)
        {
            try
            {
                return StatusCode(200, await _personBusiness.AddAsync(MSPPerson));
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<MSPPersonDTO>> Put(MSPPersonDTO MSPPerson)
        {
            try
            {
                return StatusCode(200, await _personBusiness.UpdateAsync(MSPPerson));
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<MSPPersonDTO>> Delete(MSPPersonDTO MSPPerson)
        {
            try
            {
                return StatusCode(200, await _personBusiness.DeleteAsync(MSPPerson));
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }
    }
}
