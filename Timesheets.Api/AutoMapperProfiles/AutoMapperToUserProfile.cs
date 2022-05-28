using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            CreateMap<CreateUserRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opts => opts.MapFrom(source => SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(source.Password))));
            CreateMap<UpdateUserRequestDto, User>()
                .ForMember(dest => dest.PasswordHash, opts => opts.MapFrom(source => SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(source.Password))));
            CreateMap<DeleteUserRequestDto, User>();

            CreateMap<User, ResponseUserDto>()
                .ForMember(dest => dest.PasswordHash, opts => opts.MapFrom(source => Encoding.UTF8.GetString(source.PasswordHash)));
        }
    }
}
