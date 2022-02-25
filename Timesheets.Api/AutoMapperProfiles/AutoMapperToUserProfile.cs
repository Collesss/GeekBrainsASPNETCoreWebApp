using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models.Dto.User;
using Timesheets.Models;

namespace Timesheets.Api.AutoMapperProfiles
{
    public class AutoMapperToUserProfile : Profile
    {
        public AutoMapperToUserProfile()
        {
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<DeleteUserDto, User>();
        }
    }
}
