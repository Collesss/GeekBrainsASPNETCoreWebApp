using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models.Dto.User.Request;
using Timesheets.Api.Models.Dto.User.Response;
using Timesheets.Models;
using Timesheets.Storage.Repositories;

namespace Timesheets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase // : BaseCRUDController<User, CreateUserRequestDto, UpdateUserRequestDto, DeleteUserRequestDto, ResponseUserDto>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _autoMapper;

        public UsersController(IUserRepository repository, IMapper autoMapper) //: base(repository, autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            User user = await _repository.GetById(id);

            if (user == null)
                return NotFound(id);

            return Ok(_autoMapper.Map<ResponseUserDto>(user));
        }

        [HttpGet]
        public virtual async Task<IEnumerable<ResponseUserDto>> Get() =>
            _autoMapper.Map<ResponseUserDto[]>(await _repository.GetAll());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserRequestDto createUser)
        {
            if (await _repository.GetByUsername(createUser.Username) != null)
                ModelState.AddModelError("Username", "Пользователь с таким username уже существует.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_autoMapper.Map<ResponseUserDto>(await _repository.Add(_autoMapper.Map<User>(createUser))));
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserRequestDto updateUser)
        {
            if (await _repository.GetById(updateUser.Id) == null)
                ModelState.AddModelError("Id", "Пользователь с таким id не существует.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_autoMapper.Map<ResponseUserDto>(await _repository.Update(_autoMapper.Map<User>(updateUser))));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(DeleteUserRequestDto deleteUser)
        {
            if (await _repository.GetById(deleteUser.Id) == null)
                ModelState.AddModelError("Id", "Пользователь с таким id не существует.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_autoMapper.Map<ResponseUserDto>(await _repository.Delete(_autoMapper.Map<User>(deleteUser))));
        }

    }
}
