using MSP.Domain.DTOs;
using MSP.Domain.Entities;
using MSP.Domain.Repositories;
using MSP.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MSP.Domain.BusinessInterfaces;

namespace MSP.Domain.Business
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        public PersonBusiness(IPersonRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MSPPersonDTO>> GetAllAsync(bool enabledOnly)
        {
            IEnumerable<MSPPerson> list = await _repository.GetAllAsync(enabledOnly);
            return list.ConvertAll();
        }

        public async Task<MSPPersonDTO?> AddAsync(MSPPersonDTO MSPPerson)
        {
            if (MSPPerson == null)
                return null;
            var updatedEntity = await _repository.AddAsync(MSPPerson.Convert());
            return updatedEntity?.Convert();
        }

        public async Task<MSPPersonDTO?> UpdateAsync(MSPPersonDTO MSPPerson)
        {
            if (MSPPerson == null)
                return null;

            MSPPerson? data = await _repository.GetAsync(MSPPerson.Convert());
            if (data == null)
                throw new Exception($"Setting with key {MSPPerson.PersonId} does not exist.");

            var updatedEntity = await _repository.UpdateAsync(new MSPPerson()
            {
                PersonId = data.PersonId,
                Name = MSPPerson.Name ?? data.Name,
                Login = MSPPerson.Login ?? data.Login,
                Passworld = MSPPerson.Passworld ?? data.Passworld,
                DTBegin = data.DTBegin,
                DTUpdate = data.DTUpdate,
                DTEnd = data.DTEnd
            });
            return updatedEntity?.Convert();
        }

        public async Task<MSPPersonDTO?> DeleteAsync(MSPPersonDTO? MSPPerson)
        {
            if (MSPPerson == null)
                return null;

            var deletedEntity = await _repository.DeleteAsync(MSPPerson.Convert());
            return deletedEntity?.Convert();
        }
    }
}
