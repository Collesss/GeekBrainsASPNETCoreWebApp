using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticateService
{
    public class PairTokens
    {
        public string AccessToken { get; }
        public string RefreshToken { get; }

        public PairTokens(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
