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
    public class PersonBusiness : BaseBusiness, IPersonBusiness
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

        public async Task<MSPPersonDTO?> AddAsync(MSPPersonDTO mSPPerson)
        {
            if( ValidateAsync(mSPPerson.Convert()).Result is MSPPersonDTO errorDTO)
                return errorDTO;

            MSPPerson? data = mSPPerson.Convert();
            data.Passworld = mSPPerson.Passworld?.HashPassword();

            var updatedEntity = await _repository.AddAsync(data);
            return updatedEntity?.Convert();
        }

        public async Task<MSPPersonDTO?> UpdateAsync(MSPPersonDTO mSPPerson)
        {
            MSPPerson? data = await _repository.GetByIdAsync(mSPPerson.Convert());
            if (data == null)
                return GetErrorDTO<MSPPersonDTO>("Registro não encontrado.");

            var updatedEntity = new MSPPerson()
            {
                PersonId = data.PersonId,
                Name = mSPPerson.Name ?? data.Name,
                Login = mSPPerson.Login ?? data.Login,
                Passworld = data.Passworld,
                DTBegin = data.DTBegin,
                DTUpdate = data.DTUpdate,
                DTEnd = data.DTEnd
            };
            if( ValidateAsync(updatedEntity).Result is MSPPersonDTO errorDTO)
                return errorDTO;

            return (await _repository.UpdateAsync(updatedEntity))?.Convert();
        }

        public async Task<MSPPersonDTO?> DeleteAsync(MSPPersonDTO mSPPerson)
        {
            var deletedEntity = await _repository.DeleteAsync(mSPPerson.Convert());
            return deletedEntity?.Convert();
        }

        public async Task<MSPPersonDTO?> ValidateAsync(MSPPerson entity)
        {
            if(string.IsNullOrEmpty(entity.Login))
                return GetErrorDTO<MSPPersonDTO>("O login é obrigatório.");

            MSPPerson? dbPerson = await _repository.GetByLoginAsync(entity);
            // verifica se é um novo registro
            if (entity.PersonId == 0)
            {
                // se for novo, verifica se o login já existe
                if (dbPerson != null)
                    return GetErrorDTO<MSPPersonDTO>("Já existe um usuário cadastrado com este login.");
            }

            if (entity.PersonId > 0)
            {
                // se for atualização, verifica se o login já existe para outro usuário
                if (dbPerson != null && dbPerson.PersonId != entity.PersonId)
                    return GetErrorDTO<MSPPersonDTO>("Já existe um usuário cadastrado com este login.");
            }

            return null;
        }
    }
}
