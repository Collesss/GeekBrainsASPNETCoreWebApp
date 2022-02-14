using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models;
using Timesheets.Models;
using Timesheets.Storage.Repositories;

namespace Timesheets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IRepository<Person, BaseKey> _repository;

        public PersonsController(IRepository<Person, BaseKey> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Persons([FromRoute] int id) =>
            _repository.GetAll().FirstOrDefault(p => p.Id == id) is Person person ?
            Ok(person) : (IActionResult)NotFound();

        [HttpGet]
        [Route("search")]
        public IActionResult Persons([FromQuery] string searchTerm) =>
            _repository.GetAll().FirstOrDefault(p => p.FirstName == searchTerm) is Person person ?
            Ok(person) : (IActionResult)NotFound();

        [HttpGet]
        public IActionResult Persons(PersonPageNavDto personPageNav) =>
            Ok(_repository.GetAll().Skip(personPageNav.Skip).Take(personPageNav.Take));

        [HttpPost]
        public IActionResult PersonsAdd([FromBody] Person person) =>
            _repository.Add(person) ? (IActionResult)Ok() : Conflict("id person is already created.");


        [HttpPut]
        public IActionResult PersonsUpdate([FromBody] Person person) =>
            _repository.Update(person) ? (IActionResult)Ok() : NotFound();

        [HttpDelete]
        [Route("{id}")]
        public IActionResult PersonsDelete([FromRoute] BaseKey key) =>
            _repository.Delete(key) ? (IActionResult)Ok() : NotFound();
    }
}
}
