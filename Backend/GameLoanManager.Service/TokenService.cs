using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GameLoanManager.Domain.Entities;
using GameLoanManager.Domain.Interfaces;
using GameLoanManager.Domain.Security;
using GameLoanManager.Domain.ViewModels;
using Microsoft.IdentityModel.Tokens;

namespace GameLoanManager.Service
{
    public class TokenService : ITokenService
    {
        private TokenConfigurations _tokenConfigurations;
        public TokenService(TokenConfigurations tokenConfigurations)
        {
            _tokenConfigurations = tokenConfigurations;
        }
        public TokenViewModel GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenConfigurations.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddMinutes(_tokenConfigurations.ExpirationMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var result = new TokenViewModel {
                User = user.Name,
                ExpirationDate = tokenDescriptor.Expires.Value,
                AccessToken = tokenHandler.WriteToken(token)
            };

            return result;
        }
    }
}