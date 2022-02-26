using Lesson3Project1.Models;
using Lesson3Project1.Models.Dto;
using Lesson3Project1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Lesson3Project1.Controllers
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
            _repository.GetByKey(new BaseKey { Id = id }) is Person person ? 
            Ok(person) : (IActionResult)NotFound();

        [HttpGet]
        [Route("search")]
        public IActionResult Persons([FromQuery] string searchTerm) =>
            _repository.GetAll().FirstOrDefault(p => p.FirstName == searchTerm) is Person person ? 
            Ok(person) : (IActionResult)NotFound();

        [HttpGet]
        public IActionResult Persons([FromQuery] PersonPageNavDto personPageNav) =>
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
