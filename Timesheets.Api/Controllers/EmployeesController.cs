using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models.Dto.Employee.Request;
using Timesheets.Api.Models.Dto.Employee.Response;
using Timesheets.Models;
using Timesheets.Storage.Repositories;

namespace Timesheets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseCRUDController<Employee, CreateEmployeeRequestDto, UpdateEmployeeRequestDto, DeleteEmployeeRequestDto, ResponseEmployeeDto>
    {
        public EmployeesController(IRepository<Employee> repository, IMapper autoMapper) : base(repository, autoMapper)
        {

        }

        [HttpGet("{id}")]
        public async Task<ResponseEmployeeDto> Get(int id) =>
            _autoMapper.Map<ResponseEmployeeDto>(await _repository.GetById(id));
    }
}
