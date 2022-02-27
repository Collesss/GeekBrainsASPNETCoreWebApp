using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Api.Models.Dto.Employee.Request
{
    public class UpdateEmployeeRequestDto
    {
        [Required]
        public int Id { get; set; }
        public string Department { get; set; }
        public int UserId { get; set; }
    }
}
