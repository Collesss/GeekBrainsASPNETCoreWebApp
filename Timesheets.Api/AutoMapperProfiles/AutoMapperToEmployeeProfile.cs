using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models.Dto.Employee;
using Timesheets.Models;

namespace Timesheets.Api.AutoMapperProfiles
{
    public class AutoMapperToEmployeeProfile : Profile
    {
        public AutoMapperToEmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
            CreateMap<DeleteEmployeeDto, Employee>();
        }
    }
}
