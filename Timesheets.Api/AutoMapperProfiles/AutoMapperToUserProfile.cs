using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheets.Api.Models.Dto.User;
using Timesheets.Api.Models.Dto.User.Request;
using Timesheets.Api.Models.Dto.User.Response;
using Timesheets.Models;

namespace Timesheets.Api.AutoMapperProfiles
{
    public class AutoMapperToUserProfile : Profile
    {
        public AutoMapperToUserProfile()
        {
            CreateMap<CreateUserRequestDto, User>();
            CreateMap<UpdateUserRequestDto, User>();
            CreateMap<DeleteUserRequestDto, User>();

            CreateMap<User, ResponseUserDto>();
        }
    }
}
