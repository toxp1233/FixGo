using AutoMapper;
using gizmogeo.Application.ServiceRequests.Command.CreateService;
using gizmogeo.Application.ServiceRequests.Command.UpdateService;
using gizmogeo.Application.ServiceRequests.Dtos;
using gizmogeo.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace gizmogeo.Application.ServiceRequests.Mapper;

public class ServiceRequestMappingProfile : Profile
{
    public ServiceRequestMappingProfile()
    {
        CreateMap<CreateServiceRequestCommand, ServiceRequest>()
                .ForMember(dest => dest.Attachments, opt => opt.Ignore());

        CreateMap<UpdateServiceCommand, ServiceRequest>();
        CreateMap<ServiceRequest, ServiceRequestDto>();
        CreateMap<Attachment, AttachmentsDto>();
        CreateMap<AcceptedRequest, AcceptedRequestsDto>();
        CreateMap<IFormFile, Attachment>();
    }
}
