using System;
using System.Collections.Generic;
using System.Text;

namespace AuthenticateService
{
    /*`
    public enum StatusAuthenticate
    {
        OK = 1,
        UserNotFound,
        PasswordIncorrect,
    }
    */
    public interface IUserAuthenticate
    {
        public PairTokens Authenticate(string username, string password);
        //public StatusAuthenticate TryAuthenticate(string username, string password, out PairTokens tokens);
        public PairTokens GetNewPairToken(string refreshToken);
    }
}
