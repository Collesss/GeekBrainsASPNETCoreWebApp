using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
        public Task<PairTokens> Authenticate(string username, string password);
        //public StatusAuthenticate TryAuthenticate(string username, string password, out PairTokens tokens);
        public Task<PairTokens> GetNewPairToken(string refreshToken);
    }
}
