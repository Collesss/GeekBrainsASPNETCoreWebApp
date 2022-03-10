using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Timesheets.Models;
using Timesheets.Storage.Repositories;

namespace AuthenticateService
{
    public class UserAuthenticate : IUserAuthenticate
    {
        private readonly IUserRepository _repositoryUsers;

        public UserAuthenticate(IUserRepository repositoryUsers)
        {
            _repositoryUsers = repositoryUsers;
        }

        PairTokens IUserAuthenticate.Authenticate(string username, string password) =>
            _repositoryUsers.GetByUsername(username).Result is User user && CheckPasswordHash(password, user.PasswordHash) ? new PairTokens("", "") : null;

        private bool CheckPasswordHash(string password, byte[] hash) =>
            SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(hash);

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
    }
}
