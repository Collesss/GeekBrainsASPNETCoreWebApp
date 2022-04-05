using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheets.Tokens.Models
{
    public class CommonDataTokenWithExpire<T> : CommonDataToken<T> where T : DataForGenToken
    {
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
