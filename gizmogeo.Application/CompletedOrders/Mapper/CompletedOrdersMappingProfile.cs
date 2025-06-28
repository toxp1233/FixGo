using AutoMapper;
using gizmogeo.Application.CompletedOrders.Commands;
using gizmogeo.Application.CompletedOrders.Commands.PatchNotes;
using gizmogeo.Application.CompletedOrders.Dtos;
using gizmogeo.Domain.Entities;

namespace gizmogeo.Application.CompletedOrders.Mapper;

public class CompletedOrdersMappingProfile : Profile
{
    public CompletedOrdersMappingProfile()
    {
        CreateMap<CreateCompletedOrderCommand, CompletedOrder>();

        CreateMap<CompletedOrder, CompletedOrderDto>()
               .ForMember(dest => dest.Attachments,
                    opt => opt.MapFrom(src => src.Attachments.Select(a => a.FilePath).ToList()));


        CreateMap<PatchNotesCommand, CompletedOrder>();
    }
}
