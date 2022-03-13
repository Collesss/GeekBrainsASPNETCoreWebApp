using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Tokens.Models;

namespace Timesheets.Tokens
{
    public class TokenGeneratorWithBaseData<TBaseData, VDataForGen> : ITokenGenerator<VDataForGen>
        where TBaseData : BaseDataForGenToken
        where VDataForGen : DataForGenToken
    {
        private readonly TBaseData _baseData;

        public TokenGeneratorWithBaseData(TBaseData baseData)
        {
            _baseData = baseData;
        }

        string ITokenGenerator<VDataForGen>.GetToken(VDataForGen dataForGen)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, dataForGen.UserName),
                    new Claim("TokenType", _baseData.TokenType)
                }),
                Expires = DateTime.UtcNow.Add(_baseData.LifeTime),
                SigningCredentials = new SigningCredentials(_baseData.SingingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        async Task<bool> ITokenGenerator<VDataForGen>.CheckValidToken(string token) 
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            var ValidResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _baseData.Issuer,
                ValidateAudience = true,
                ValidAudience = _baseData.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _baseData.SingingKey,
                ValidateLifetime = true
            });

            return ValidResult.IsValid && ValidResult.ClaimsIdentity.HasClaim("TokenType", _baseData.TokenType);
        }
    }
}
