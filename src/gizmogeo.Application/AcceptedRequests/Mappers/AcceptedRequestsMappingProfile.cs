using AutoMapper;
using gizmogeo.Application.AcceptedRequests.Commands.UpdateAcceptedRequest;
using gizmogeo.Application.AcceptedRequests.Dtos;

namespace gizmogeo.Application.AcceptedRequests.Mappers;

public class AcceptedRequestsMappingProfile : Profile
{
    public AcceptedRequestsMappingProfile()
    {
        CreateMap<AcceptedRequest, AcceptedRequestByUserDto>()
              .ForMember(dest => dest.CategoryName,
               opt => opt.MapFrom(src => src.ServiceRequest.Category.Name))
              .ForMember(dest => dest.AttachmentUrls,
               opt => opt.MapFrom(src => src.Attachments.Select(a => a.FilePath).ToList()));
        CreateMap<UpdateAcceptedRequestCommand, AcceptedRequest>();

    }
}
