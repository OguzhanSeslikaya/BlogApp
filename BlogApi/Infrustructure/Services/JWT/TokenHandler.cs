using Blog.Application.Abstractions.JWT;
using Blog.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrustructure.Services.JWT
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token createAccessToken(int minute,AppUser user)
        {
            Token token = new Token();

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["token:securityKey"]));
            SigningCredentials signingCredentials = new(key,SecurityAlgorithms.HmacSha256);
            token.expiration = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken securityToken = new(
                audience: _configuration["token:audience"],
                issuer: _configuration["token:issuer"],
                expires: token.expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, user.UserName) }
                );
            JwtSecurityTokenHandler securityTokenHandler = new();
            token.accessToken = securityTokenHandler.WriteToken(securityToken);

            token.refreshToken = createRefreshToken();
            return token;
        }

        public string createRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
