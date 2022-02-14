using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Api.Models
{
    public class PersonPageNavDto
    {
        [MinLength(0, ErrorMessage = "{0} cant be less 0")]
        public int Skip { get; set; } = 0;

        [MinLength(0, ErrorMessage = "{0} cant be less 0")]
        public int Take { get; set; } = 10;
    }
}
