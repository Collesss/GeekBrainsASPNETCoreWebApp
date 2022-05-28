using AutoMapper;
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
    public class UsersController : BaseCRUDController<User, CreateUserRequestDto, UpdateUserRequestDto, DeleteUserRequestDto, ResponseUserDto>
    {
        public UsersController(IRepository<User> repository, IMapper autoMapper) : base(repository, autoMapper)
        {

        }

        [HttpGet("{id}")]
        public async Task<ResponseUserDto> Get(int id) =>
            _autoMapper.Map<ResponseUserDto>(await _repository.GetById(id));
        
        /*
        public override async Task<ResponseUserDto> Post([FromBody] CreateUserRequestDto createEmployee)
        {
            _autoMapper.Map<User>(createEmployee)


            return await base.Post(createEmployee);
        }
        */
    }
}
