using AutoMapper;
using gizmogeo.Application.Users.Commands.CreateUser;
using gizmogeo.Application.Users.Commands.UpdateUser;
using gizmogeo.Application.Users.Dtos;

namespace gizmogeo.Application.Users.Mappers;

public class UsersMappingProfile : Profile
{
    public UsersMappingProfile()
    {
        CreateMap<CreateUserCommand, User>();
        CreateMap<UpdateUserCommand, User>();
        CreateMap<User, UserDto>()
       .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));

    }
}
