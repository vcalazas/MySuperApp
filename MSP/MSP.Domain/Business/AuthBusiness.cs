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

        public AuthBusiness(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MSPAuthDTO>> GetAllAsync(bool enabledOnly)
        {
            IEnumerable<MSPAuth> list = await _repository.GetAllAsync(enabledOnly);
            return list.ConvertAll();
        }

        public async Task<MSPAuthDTO?> AddAsync(MSPAuthDTO entity)
        {
            if(ValidateAsync(entity.Convert()).Result is MSPAuthDTO errorDTO)
                return errorDTO;
            var updatedEntity = await _repository.AddAsync(entity.Convert());
            return updatedEntity?.Convert();
        }

        public async Task<MSPAuthDTO?> UpdateAsync(MSPAuthDTO entity)
        {
            MSPAuth? data = await _repository.GetByIdAsync(entity.Convert());
            if (data == null)
                return GetErrorDTO<MSPAuthDTO>("Registro não encontrado.");

            var updatedEntity = new MSPAuth()
            {
                AuthId = data.AuthId,
                PersonId = data.PersonId,

                DeviceType = entity.DeviceType,
                SO = entity.SO,
                Manufacturer = entity.Manufacturer,
                Model = entity.Model,
                Version = entity.Version,

                DTBegin = data.DTBegin,
                DTUpdate = data.DTUpdate,
                DTEnd = data.DTEnd
            };
            if( ValidateAsync(updatedEntity).Result is MSPAuthDTO errorDTO)
                return errorDTO;

            return (await _repository.UpdateAsync(updatedEntity))?.Convert();
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
            MSPAuthDTO token = await CreateJWTToken(dto);



            return new MSPAuthDTO()
            {
                AccessToken = token.AccessToken,
                ExpiresIn = token.ExpiresIn,
            };
        }

        public async Task<MSPAuthDTO> CreateJWTToken(MSPAuthDTO request)
        {
            string secretToken = GenerateSecretToken();
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(request.JwtConfigIssuerTokenValidiyMins??0);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sid, secretToken)
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = request.JwtConfigIssuer,
                Audience = request.JwtConfigAudience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(request.JwtConfigIssuerKey)), SecurityAlgorithms.HmacSha512Signature),
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
