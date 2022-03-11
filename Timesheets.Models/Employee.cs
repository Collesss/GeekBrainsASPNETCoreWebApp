using System;
using System.Collections.Generic;
using System.Text;

namespace Timesheets.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}