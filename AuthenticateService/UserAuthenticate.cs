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
        private readonly ITokenGenerator<DataForGenAccessToken, CommonDataTokenWithExpire<DataForGenAccessToken>> _tokenGeneratorAccess;
        private readonly ITokenGenerator<DataForGenRefreshToken, CommonDataTokenWithExpire<DataForGenRefreshToken>> _tokenGeneratorRefresh;

        public UserAuthenticate(IUserRepository repositoryUsers, 
                                ITokenGenerator<DataForGenAccessToken, CommonDataTokenWithExpire<DataForGenAccessToken>> tokenGeneratorAccess, 
                                ITokenGenerator<DataForGenRefreshToken, CommonDataTokenWithExpire<DataForGenRefreshToken>> tokenGeneratorRefresh)
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

        PairTokens IUserAuthenticate.GetNewPairToken(string refreshToken) =>
                _tokenGeneratorRefresh.TryCheckValidToken(refreshToken, out CommonDataTokenWithExpire<DataForGenRefreshToken> dataToken) ?
                new PairTokens(_tokenGeneratorAccess.GetToken(new DataForGenAccessToken { UserName = dataToken.DataForGen.UserName }),
                               _tokenGeneratorRefresh.GetToken(new DataForGenRefreshToken { UserName = dataToken.DataForGen.UserName })) :
                null;

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
