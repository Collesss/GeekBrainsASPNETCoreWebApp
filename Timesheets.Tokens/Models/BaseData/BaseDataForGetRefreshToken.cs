using System;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;

namespace Timesheets.Tokens.Models
{
    public sealed class BaseDataForGetRefreshToken : BaseDataForGenToken
    {
        public override string TokenType => "Refresh";

        public BaseDataForGetRefreshToken(string issuer, string audience, TimeSpan lifeTime, SecurityKey singingKey) 
            : base(issuer, audience, lifeTime, singingKey) { }
    }
}
