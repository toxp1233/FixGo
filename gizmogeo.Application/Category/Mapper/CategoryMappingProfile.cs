using AutoMapper;
using gizmogeo.Domain.Entities;
using gizmogeo.Application.Category.Dtos;
using gizmogeo.Application.Category.Commands.CreateCategory;
using gizmogeo.Application.Category.Commands.UpdateCategory;

namespace gizmogeo.Application.Category.Mapper;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<gizmogeo.Domain.Entities.Category, CategoryDto>().ReverseMap();

        CreateMap<ServiceRequest, ServiceRequestDto>();

        CreateMap<AcceptedRequest, AcceptedRequestDto>();

        CreateMap<CreateCategoryCommand, gizmogeo.Domain.Entities.Category>();
        CreateMap<UpdateCategoryCommand, gizmogeo.Domain.Entities.Category>();
    }
}
