using AutoMapper;
using gizmogeo.Application.Auth.Commands.RegisterUser;
using gizmogeo.Application.Auth.Dtos;

namespace gizmogeo.Application.Auth.Mapper;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<RegisterUserCommand, User>().ReverseMap();
        CreateMap<User, TokenDto>();
    }
}
