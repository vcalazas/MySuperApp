using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MSP.Domain.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MSP.API.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<UserDto> GenerateToken(UserDto user)
        {
            return await Authenticate(user, GenerateSecretToken());
        }

        public async Task<UserDto> GenerateRefreshToken(string? token, UserDto user) {
            return await Authenticate(user, GenerateSecretToken());
        }

        public async Task<UserDto> Authenticate(UserDto request, string secretToken)
        {
            var issuer = _configuration["JwtConfig:Issuer"];
            var audience = _configuration["JwtConfig:Audience"];
            var key = _configuration["JwtConfig:Key"];
            var tokenValidityMins = _configuration.GetValue<int>("JwtConfig:TokenValidiyMins");
            var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Name, request.UserName),
                    new Claim(JwtRegisteredClaimNames.Sid, secretToken)
                }),
                Expires = tokenExpiryTimeStamp,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),SecurityAlgorithms.HmacSha512Signature),

            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(securityToken);

            return new UserDto()
            {
                AccessToken = accessToken,
                UserName = request.UserName,
                ExpiresIn = (int) tokenExpiryTimeStamp.Subtract(DateTime.UtcNow).TotalSeconds
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
