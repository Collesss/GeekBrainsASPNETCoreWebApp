using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson3Project1.Models.Dto
{
    public class PersonPageNavDto
    {
        [Range(0, int.MaxValue, ErrorMessage = "{0} cant be less 0")]
        public int Skip { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "{0} cant be less 0")]
        public int Take { get; set; } = 10;
    }
}
