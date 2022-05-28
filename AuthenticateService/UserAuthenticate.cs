using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        async Task<PairTokens> IUserAuthenticate.Authenticate(string username, string password)
        {
            if (await _repositoryUsers.GetByUsername(username) is User user && CheckPasswordHash(password, user.PasswordHash))
            {
                PairTokens pairTokens = new PairTokens(_tokenGeneratorAccess.GetToken(new DataForGenAccessToken { UserName = user.Username }),
                           _tokenGeneratorRefresh.GetToken(new DataForGenRefreshToken { UserName = user.Username }));

                user.LastRefreshToken = pairTokens.RefreshToken;

                await _repositoryUsers.Update(user);

                return pairTokens;
            }

            return null;
        }
        private bool CheckPasswordHash(string password, byte[] hash) =>
            SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(hash);

        async Task<PairTokens> IUserAuthenticate.GetNewPairToken(string refreshToken)
        {
            if(_tokenGeneratorRefresh.TryCheckValidToken(refreshToken, out CommonDataTokenWithExpire<DataForGenRefreshToken> dataToken) &&
                await _repositoryUsers.GetByUsername(dataToken.DataForGen.UserName) is User user)
            {
                PairTokens pairTokens = new PairTokens(_tokenGeneratorAccess.GetToken(new DataForGenAccessToken { UserName = dataToken.DataForGen.UserName }),
                               _tokenGeneratorRefresh.GetToken(new DataForGenRefreshToken { UserName = dataToken.DataForGen.UserName }));

                user.LastRefreshToken = pairTokens.RefreshToken;

                await _repositoryUsers.Update(user);

                return pairTokens;
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
