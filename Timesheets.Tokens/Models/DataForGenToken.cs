using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheets.Tokens.Models
{
    public abstract class DataForGenToken
    {
        public string UserName { get; private set; }

        public DataForGenToken(string userName)
        {
            UserName = userName;
        }
    }
}
