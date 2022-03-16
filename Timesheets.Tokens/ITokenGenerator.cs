using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Timesheets.Tokens
{
    public interface ITokenGenerator<T>
    {
        string GetToken(T dataForGen);

        Task<bool> CheckValidToken(string token);
        bool TryCheckValidToken(string token, out T dataToken);
    }
}
