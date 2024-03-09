using Deneme.Interface;
using GorevYoneticisi.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Deneme.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task<TokenInfo> GenerateToken(Users user)
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]));
            var dateTimeNow = DateTime.Now;

            var jwt = new JwtSecurityToken(
                issuer: _configuration["AppSettings:ValidIssuer"],
                audience: _configuration["AppSettings:ValidAudience"],
                claims: new List<Claim>
                {
                    new Claim("username", user.UserName)
                },
                notBefore: dateTimeNow,
                expires: dateTimeNow.AddDays(7),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            var tokenInfo = new TokenInfo
            {
                userid = user.Id,
                Token = token,
                ExpiryTime = jwt.ValidTo
            };

            return Task.FromResult(tokenInfo);
        }
    }
}
