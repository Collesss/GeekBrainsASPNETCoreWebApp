using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public abstract class BaseCRUDController<TEntity, RQCreateRequestDto, RQUpdateRequestDto, RQDeleteRequestDto, RSResponseDto> : ControllerBase 
        where TEntity : class
        where RQCreateRequestDto : class
        where RQUpdateRequestDto : class
        where RQDeleteRequestDto : class
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _autoMapper;

        protected BaseCRUDController(IRepository<TEntity> repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        [HttpGet]
        public virtual async Task<IEnumerable<RSResponseDto>> Get() =>
            _autoMapper.Map<RSResponseDto[]>(await _repository.GetAll());

        //[AllowAnonymous]
        [HttpPost]
        public virtual async Task<RSResponseDto> Post([FromBody] RQCreateRequestDto createEmployee) =>
            _autoMapper.Map<RSResponseDto>(await _repository.Add(_autoMapper.Map<TEntity>(createEmployee)));

        [HttpPatch]
        public virtual async Task<RSResponseDto> Put([FromBody] RQUpdateRequestDto updateEmployee) =>
            _autoMapper.Map<RSResponseDto>(await _repository.Update(_autoMapper.Map<TEntity>(updateEmployee)));

        [HttpDelete("{id}")]
        public virtual async Task<RSResponseDto> Delete(RQDeleteRequestDto deleteEmployee) =>
            _autoMapper.Map<RSResponseDto>(await _repository.Delete(_autoMapper.Map<TEntity>(deleteEmployee)));
    }
}
