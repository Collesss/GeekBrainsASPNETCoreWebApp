using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models.Dto.User;
using Timesheets.Models;
using Timesheets.Storage.Repositories;

namespace Timesheets.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseCRUDController<User, CreateUserDto, UpdateUserDto, DeleteUserDto>
    {
        public UsersController(IRepository<User> repository, IMapper autoMapper) : base(repository, autoMapper)
        {

        }

        [HttpGet("{id}")]
        public async Task<User> Get(int id) =>
            await _repository.GetAll().FirstOrDefaultAsync(u => u.Id == id);
    }
}
