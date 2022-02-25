using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models.Dto.Employee;
using Timesheets.Models;
using Timesheets.Storage.Repositories;

namespace Timesheets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseCRUDController<Employee, CreateEmployeeDto, UpdateEmployeeDto, DeleteEmployeeDto>
    {
        public EmployeesController(IRepository<Employee> repository, IMapper autoMapper) : base(repository, autoMapper)
        {

        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(int id) =>
            await _repository.GetAll().FirstOrDefaultAsync(e => e.Id == id);
    }
}
