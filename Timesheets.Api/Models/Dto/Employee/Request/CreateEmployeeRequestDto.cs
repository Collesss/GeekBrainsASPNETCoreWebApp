using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Api.Models.Dto.Employee.Request
{
    public class CreateEmployeeRequestDto
    {
        [Required]
        public string Department { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
