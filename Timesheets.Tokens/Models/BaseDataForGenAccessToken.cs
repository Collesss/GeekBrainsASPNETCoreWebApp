using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheets.Tokens.Models
{
    public sealed class BaseDataForGenAccessToken : BaseDataForGenToken
    {
        public override string TokenType => "Access";
        
        public BaseDataForGenAccessToken(string issuer, string audience, TimeSpan lifeTime, SecurityKey singingKey) 
            : base(issuer, audience, lifeTime, singingKey) { }
    }
}
