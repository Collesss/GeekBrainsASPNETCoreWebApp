using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models.Dto.User.Request;
using Timesheets.Models;
using Timesheets.Storage.Repositories;

namespace Timesheets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseCRUDController<User, CreateUserRequestDto, UpdateUserRequestDto, DeleteUserRequestDto>
    {
        public UsersController(IRepository<User> repository, IMapper autoMapper) : base(repository, autoMapper)
        {

        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id) =>
            (await _repository.GetAll()).FirstOrDefault(u => u.Id == id);
    }
}
