using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models.Dto.Employee.Request;
using Timesheets.Models;
using Timesheets.Storage.Repositories;

namespace Timesheets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseCRUDController<Employee, CreateEmployeeRequestDto, UpdateEmployeeRequestDto, DeleteEmployeeRequestDto>
    {
        public EmployeesController(IRepository<Employee> repository, IMapper autoMapper) : base(repository, autoMapper)
        {

        }

        [HttpGet("{id}")]
        public async Task<Employee> Get(int id) =>
            (await _repository.GetAll()).FirstOrDefault(e => e.Id == id);
    }
}
