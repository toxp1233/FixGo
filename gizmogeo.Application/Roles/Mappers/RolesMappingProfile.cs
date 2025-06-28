using AutoMapper;
using gizmogeo.Application.Roles.Commands.CreateRole;
using gizmogeo.Application.Roles.Commands.UpdateRole;
using gizmogeo.Application.Roles.Dtos;
using gizmogeo.Domain.Entities;

namespace gizmogeo.Application.Roles.Mappers;

public class RolesMappingProfile : Profile
{
    public RolesMappingProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<Role, RolesDto>();
        CreateMap<CreateRoleCommand, Role>();
        CreateMap<UpdateRoleCommand, Role>();
    }
}
