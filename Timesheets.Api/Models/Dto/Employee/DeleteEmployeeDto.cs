using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Api.Models.Dto.Employee
{
    public class DeleteEmployeeDto
    {
        [Required]
        public int Id { get; set; }
    }
}
