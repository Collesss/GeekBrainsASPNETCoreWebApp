using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class EmployeesController : ControllerBase //BaseCRUDController<Employee, CreateEmployeeRequestDto, UpdateEmployeeRequestDto, DeleteEmployeeRequestDto, ResponseEmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _autoMapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IUserRepository userRepository, IMapper autoMapper)
        {
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _autoMapper = autoMapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Employee employee = await _employeeRepository.GetById(id);

            if (employee == null)
                return NotFound(id);

            return Ok(_autoMapper.Map<ResponseEmployeeDto>(employee));
        }

        [HttpGet]
        public async Task<IEnumerable<ResponseEmployeeDto>> Get() =>
            _autoMapper.Map<ResponseEmployeeDto[]>(await _employeeRepository.GetAll());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployeeRequestDto createEmployee)
        {
            if(await _employeeRepository.GetByUserId(createEmployee.UserId) != null)
                ModelState.AddModelError("UserId", "уже есть сотрудник с таким же UserId.");

            if (_userRepository.GetById(createEmployee.UserId) == null)
                ModelState.AddModelError("UserId", "не существует пользователя с таким UserId.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_autoMapper.Map<ResponseEmployeeDto>(_employeeRepository.Add(_autoMapper.Map<Employee>(createEmployee))));
        }

        [HttpPatch]
        public async Task<IActionResult> Put([FromBody] UpdateEmployeeRequestDto updateEmployee) 
        {
            if (await _employeeRepository.GetById(updateEmployee.Id) == null)
                ModelState.AddModelError("Id", "не существет сотрудника с таким Id.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_autoMapper.Map<ResponseEmployeeDto>(_employeeRepository.Update(_autoMapper.Map<Employee>(updateEmployee))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteEmployeeRequestDto deleteEmployee)
        {
            if (await _employeeRepository.GetById(deleteEmployee.Id) == null)
                ModelState.AddModelError("Id", "не существет сотрудника с таким Id.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_autoMapper.Map<ResponseEmployeeDto>(_employeeRepository.Delete(_autoMapper.Map<Employee>(deleteEmployee))));
        }
    }
}
