using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Storage.Repositories;

namespace Timesheets.Api.Controllers
{
    public abstract class BaseCRUDController<Entity, CreateDto, UpdateDto, DeleteDto> : ControllerBase 
        where Entity : class
        where CreateDto : class
        where UpdateDto : class
        where DeleteDto : class
    {
        protected readonly IRepository<Entity> _repository;
        protected readonly IMapper _autoMapper;

        protected BaseCRUDController(IRepository<Entity> repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        [HttpGet()]
        public async Task<IEnumerable<Entity>> Get() =>
            await _repository.GetAll();

        [HttpPost]
        public async Task<Entity> Post([FromBody] CreateDto createEmployee) =>
            await _repository.Add(_autoMapper.Map<Entity>(createEmployee));

        [HttpPatch]
        public async Task<Entity> Put([FromBody] UpdateDto updateEmployee) =>
            await _repository.Update(_autoMapper.Map<Entity>(updateEmployee));

        [HttpDelete("{id}")]
        public async Task<Entity> Delete(DeleteDto deleteEmployee) =>
            await _repository.Delete(_autoMapper.Map<Entity>(deleteEmployee));
    }
}
