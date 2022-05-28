using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Tokens.Models;

namespace Timesheets.Tokens
{
    public interface ITokenGenerator<T, V> where V : CommonDataToken<T>
    {
        string GetToken(T dataForGen);
        Task<bool> CheckValidToken(string token);
        bool TryCheckValidToken(string token, out V dataToken);
    }
}
