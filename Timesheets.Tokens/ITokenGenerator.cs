using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheets.Tokens
{
    public interface ITokenGenerator<T>
    {
        string GetToken(T dataForGen);
    }
}
