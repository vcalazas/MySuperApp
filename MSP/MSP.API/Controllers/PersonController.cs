using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MSP.API.Services;
using MSP.Domain.BusinessInterfaces;
using MSP.Domain.DTOs;
using MSP.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerResponse(200, "Há registros para mostrar", typeof(IEnumerable<MSPPersonDTO>))]
        [SwaggerResponse(201, "Sem registros para mostrar", typeof(IEnumerable<MSPPersonDTO>))]
        public async Task<ActionResult<IEnumerable<MSPPersonDTO>>>  Get()
        {
            return await HttpCustomValidator.IsValidListAsync<MSPPersonDTO>(async () => await _personBusiness.GetAllAsync(false));
        }

        [HttpGet("enabled")]
        [SwaggerResponse(200, "Há registros para mostrar", typeof(IEnumerable<MSPPersonDTO>))]
        [SwaggerResponse(201, "Sem registros para mostrar", typeof(IEnumerable<MSPPersonDTO>))]
        public async Task<ActionResult<IEnumerable<MSPPersonDTO>>> GetEnabled()
        {
            return await HttpCustomValidator.IsValidListAsync<MSPPersonDTO>(async () => await _personBusiness.GetAllAsync(true));
        }

        [HttpPost]
        [SwaggerResponse(200, "Pessoa registrada", typeof(MSPPersonDTO))]
        [SwaggerResponse(422, "Falha na validação", typeof(MSPPersonDTO))]
        public async Task<ActionResult<MSPPersonDTO>> Post(MSPPersonDTO MSPPerson)
        {
            return await HttpCustomValidator.IsValidAsync<MSPPersonDTO>(async () => await _personBusiness.AddAsync(MSPPerson));
        }

        [HttpPut]
        [SwaggerResponse(200, "Pessoa atualizada", typeof(MSPPersonDTO))]
        [SwaggerResponse(422, "Falha na validação", typeof(MSPPersonDTO))]
        public async Task<ActionResult<MSPPersonDTO>> Put(MSPPersonDTO MSPPerson)
        {
            return await HttpCustomValidator.IsValidAsync<MSPPersonDTO>(async () => await _personBusiness.UpdateAsync(MSPPerson));
        }

        [HttpDelete]
        public async Task<ActionResult<MSPPersonDTO>> Delete(MSPPersonDTO MSPPerson)
        {
            return await HttpCustomValidator.IsValidAsync<MSPPersonDTO>(async () => await _personBusiness.DeleteAsync(MSPPerson));
        }
    }
}
