using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheets.Api.Models.Dto.Employee.Response
{
    public class ResponseEmployeeDto
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public int UserId { get; set; }
    }
}
