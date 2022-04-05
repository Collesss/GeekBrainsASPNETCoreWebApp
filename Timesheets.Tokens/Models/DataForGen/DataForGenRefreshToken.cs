using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheets.Tokens.Models
{
    public class DataForGenRefreshToken : DataForGenToken
    {
        public DataForGenRefreshToken() { }
        public DataForGenRefreshToken(string userName) : base(userName) { }
    }
}
