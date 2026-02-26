using Microsoft.IdentityModel.Tokens;
using MSP.Domain.BusinessInterfaces;
using MSP.Domain.DTOs;
using MSP.Domain.Entities;
using MSP.Domain.Extensions;
using MSP.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MSP.Domain.Business
{
    public class AuthBusiness : BaseBusiness, IAuthBusiness
    {
        private readonly IAuthRepository _repository;
        private readonly IPersonRepository _personRepository;

        public AuthBusiness(IAuthRepository repository, IPersonRepository personRepository)
        {
            _repository = repository;
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<MSPAuthDTO>> GetAllAsync(bool enabledOnly)
        {
            IEnumerable<MSPAuth> list = await _repository.GetAllAsync(enabledOnly);
            return list.ConvertAll();
        }

        public async Task<MSPAuthDTO?> AddAsync(MSPAuthDTO entity)
        {
            return GetErrorDTO<MSPAuthDTO>("Não é possivel criar um login.");
        }

        public async Task<MSPAuthDTO?> UpdateAsync(MSPAuthDTO entity)
        {
            return GetErrorDTO<MSPAuthDTO>("Não é possivel editar um login.");
        }

        public async Task<MSPAuthDTO?> DeleteAsync(MSPAuthDTO entity)
        {
            var deletedEntity = await _repository.DeleteAsync(entity.Convert());
            return deletedEntity?.Convert();
        }

        public async Task<MSPAuthDTO?> ValidateAsync(MSPAuth entity)
        {
            return null;
        }

        public async Task<MSPAuthDTO?> LoginAsync(MSPAuthDTO dto)
        {

            MSPPerson? person = await _personRepository.GetByLoginAsync(new MSPPerson() { 
                Login = dto.PersonLogin,
            });

            if (person == null || string.IsNullOrEmpty(person.Passworld) || !person.Passworld.Equals(dto.PersonPassworld?.HashPassword()))
                return GetErrorDTO<MSPAuthDTO>("Login ou senha inválidos.");
            dto.PersonId = person.PersonId;

            return await LoginInternal(dto, person);
        }

        public async Task<MSPAuthDTO?> UpdateAsync(string? authorizationHeaderValue, MSPAuthDTO dto)
        {
            if (string.IsNullOrEmpty(authorizationHeaderValue) || !authorizationHeaderValue.StartsWith("Bearer "))
                return GetErrorDTO<MSPAuthDTO>("Não há tokem a ser validado.");

            MSPAuth? temp = await _repository.GetByTokenAsync(new MSPAuth() { 
                AccessToken = authorizationHeaderValue.Substring("Bearer ".Length).Trim() });

            if(temp == null)
                return GetErrorDTO<MSPAuthDTO>("Token inválido.");

            MSPPerson? person = await _personRepository.GetByLoginAsync(new MSPPerson(){Login = dto.PersonLogin});
            if (person == null)
                return GetErrorDTO<MSPAuthDTO>("Autenticação inválida.");

            dto.PersonId = temp.PersonId;

            await _repository.DeleteAsync(temp);

            return await LoginInternal(dto, person);
        }

        private async Task<MSPAuthDTO> LoginInternal(MSPAuthDTO dto, MSPPerson person)
        {
            MSPAuthDTO token = await CreateJWTToken(dto, person);

            MSPAuth mSPAuth = new MSPAuth()
            {
                PersonId = dto.PersonId,
                AuthId = dto.AuthId,
                DeviceType = dto.DeviceType,
                SO = dto.SO,
                Manufacturer = dto.Manufacturer,
                Model = dto.Model,
                Version = dto.Version,
                AccessToken = token.AccessToken,
            };

            if (ValidateAsync(mSPAuth).Result is MSPAuthDTO errorDTO)
                return errorDTO;

            await _repository.AddAsync(mSPAuth);

            return new MSPAuthDTO()
            {
                AccessToken = token.AccessToken,
                ExpiresIn = token.ExpiresIn,
            };
        }

        public async Task<MSPAuthDTO> CreateJWTToken(MSPAuthDTO request, MSPPerson person)
        {
            string secretToken = GenerateSecretToken();
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(request.JwtConfigIssuerTokenValidiyMins??0);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("PersonId", person.PersonId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Sid, secretToken),
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = request.JwtConfigIssuer,
                Audience = request.JwtConfigAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(request.JwtConfigIssuerKey)), SecurityAlgorithms.HmacSha256),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new MSPAuthDTO()
            {
                AccessToken = accessToken,
                ExpiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
            };
        }

        public string GenerateSecretToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
