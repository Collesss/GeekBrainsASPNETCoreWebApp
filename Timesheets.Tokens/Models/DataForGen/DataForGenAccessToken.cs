using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheets.Tokens.Models
{
    public class DataForGenAccessToken : DataForGenToken
    {
        public DataForGenAccessToken() { }
        public DataForGenAccessToken(string userName) : base(userName) { }
    }
}
