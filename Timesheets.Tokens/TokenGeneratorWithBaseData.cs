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
        where VDataForGen : DataForGenToken, new()
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

        private async Task<TokenValidationResult> GetTokenValidResult(string token) =>
            await new JwtSecurityTokenHandler().ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _baseData.Issuer,
                ValidateAudience = true,
                ValidAudience = _baseData.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _baseData.SingingKey,
                ValidateLifetime = true
            });

        async Task<bool> ITokenGenerator<VDataForGen>.CheckValidToken(string token) 
        {
            TokenValidationResult validationResult = await GetTokenValidResult(token);

            return validationResult.IsValid && validationResult.ClaimsIdentity.HasClaim("TokenType", _baseData.TokenType);
        }

        bool ITokenGenerator<VDataForGen>.TryCheckValidToken(string token, out VDataForGen dataToken)
        {
            TokenValidationResult validationResult = GetTokenValidResult(token).Result;

            if(validationResult.IsValid && validationResult.ClaimsIdentity.HasClaim("TokenType", _baseData.TokenType))
            {
                dataToken = new VDataForGen 
                { 
                    UserName = validationResult.ClaimsIdentity.FindFirst(ClaimTypes.Name).Value
                };

                return true;
            }

            dataToken = null;

            return false;
        }
    }
}
