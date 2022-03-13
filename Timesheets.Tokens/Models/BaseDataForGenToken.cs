using System;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;

namespace Timesheets.Tokens.Models
{
    public abstract class BaseDataForGenToken
    {
        public abstract string TokenType { get; }
        public string Issuer { get; private set; }
        public string Audience { get; private set; }
        public TimeSpan LifeTime { get; private set; }
        public SecurityKey SingingKey { get; private set; }

        public BaseDataForGenToken(string issuer, string audience, TimeSpan lifeTime, SecurityKey singingKey)
        {
            Issuer = issuer;
            Audience = audience;
            LifeTime = lifeTime;
            SingingKey = singingKey;
        }
    }
}
