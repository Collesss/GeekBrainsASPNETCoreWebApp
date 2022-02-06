using Lesson3Project1.Models;
using Lesson3Project1.Models.Dto;
using Lesson3Project1.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson3Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IRepository<Person> _repository;

        public PersonsController(IRepository<Person> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Persons([FromRoute]int id)
        {
            Person person = _repository.GetAll().FirstOrDefault(p => p.Id == id);
            
            return person is null ? (IActionResult)NotFound() : Ok(person);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Persons([FromQuery] string searchTerm)
        {
            Person person = _repository.GetAll().FirstOrDefault(p => p.FirstName == searchTerm);

            return person is null ? (IActionResult)NotFound() : Ok(person);
        }

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
        public IActionResult PersonsDelete([FromRoute] int id) =>
            _repository.Delete(id) ? (IActionResult)Ok() : NotFound();
    }
}
