using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models.Dto.Employee.Request;
using Timesheets.Api.Models.Dto.Employee.Response;
using Timesheets.Models;

namespace Timesheets.Api.AutoMapperProfiles
{
    public class AutoMapperToEmployeeProfile : Profile
    {
        public AutoMapperToEmployeeProfile()
        {
            CreateMap<CreateEmployeeRequestDto, Employee>();
            CreateMap<UpdateEmployeeRequestDto, Employee>();
            CreateMap<DeleteEmployeeRequestDto, Employee>();

            CreateMap<User, ResponseEmployeeDto>();
        }
    }
}
