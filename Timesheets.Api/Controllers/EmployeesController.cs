using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Models;
using Timesheets.Storage.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timesheets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IRepository<Employee> _repository;
        public EmployeesController(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        public async Task<IEnumerable<Employee>> Get() =>
            await _repository.GetAll().ToListAsync();

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public async Task<Employee> Get(int id) =>
            await _repository.GetAll().FirstOrDefaultAsync(e => e.Id == id);

        // POST api/<EmployeesController>
        [HttpPost]
        public Employee Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
