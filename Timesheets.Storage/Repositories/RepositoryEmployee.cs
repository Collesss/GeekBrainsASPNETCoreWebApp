using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Models;

namespace Timesheets.Storage.Repositories
{
    public class RepositoryEmployee : Repository<Employee>, IEmployeeRepository
    {
        public RepositoryEmployee(TimeSheetDbContext context) : base(context) { }

        public override async Task<Employee> GetById(int Id) =>
            await _context.Employees.FirstOrDefaultAsync(e => e.Id == Id);

        async Task<Employee> IEmployeeRepository.GetByUserId(int id) =>
            await _context.Employees.FirstOrDefaultAsync(e => e.UserId == id);
    }
}