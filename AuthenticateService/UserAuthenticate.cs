using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Timesheets.Models;
using Timesheets.Storage.Repositories;
using Timesheets.Tokens;
using Timesheets.Tokens.Models;

namespace AuthenticateService
{
    public class UserAuthenticate : IUserAuthenticate
    {
        private readonly IUserRepository _repositoryUsers;
        private readonly ITokenGenerator<DataForGenAccessToken> _tokenGeneratorAccess;
        private readonly ITokenGenerator<DataForGenRefreshToken> _tokenGeneratorRefresh;

        public UserAuthenticate(IUserRepository repositoryUsers, 
                                ITokenGenerator<DataForGenAccessToken> tokenGeneratorAccess, 
                                ITokenGenerator<DataForGenRefreshToken> tokenGeneratorRefresh)
        {
            _repositoryUsers = repositoryUsers;
            _tokenGeneratorAccess = tokenGeneratorAccess;
            _tokenGeneratorRefresh = tokenGeneratorRefresh;
        }

        PairTokens IUserAuthenticate.Authenticate(string username, string password) =>
            _repositoryUsers.GetByUsername(username).Result is User user && CheckPasswordHash(password, user.PasswordHash) ? 
            new PairTokens(_tokenGeneratorAccess.GetToken(new DataForGenAccessToken { UserName = user.Username }), 
                           _tokenGeneratorRefresh.GetToken(new DataForGenRefreshToken { UserName = user.Username })) : 
            null;

        private bool CheckPasswordHash(string password, byte[] hash) =>
            SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(hash);

        PairTokens IUserAuthenticate.GetNewPairToken(string refreshToken)
        {
            if(_tokenGeneratorRefresh.TryCheckValidToken(refreshToken, out DataForGenRefreshToken dataToken))
            {

            }

            return null;
        }

        /*
        StatusAuthenticate IUserAuthenticate.TryAuthenticate(string username, string password, out PairTokens tokens)
        {
            tokens = null;

            User user = _repositoryUsers.GetByUsername(username).Result;

            if (user is null)
                return StatusAuthenticate.UserNotFound;

            if (!CheckPasswordHash(password, user.PasswordHash))
                return StatusAuthenticate.PasswordIncorrect;

            tokens = new PairTokens("", "");

            return StatusAuthenticate.OK;
        }
        */
    }
}
