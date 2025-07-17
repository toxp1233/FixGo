using AutoMapper;
using gizmogeo.Application.ServiceRequests.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using gizmogeo.Infrastructure.Services;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Command.CreateService;

public class CreateServiceRequestCommandHandler(IServiceRequestRepository serviceRequestRepository, IMapper mapper, ICloudinaryService cloudinaryService) : IRequestHandler<CreateServiceRequestCommand, ServiceRequestDto>
{
    public async Task<ServiceRequestDto> Handle(CreateServiceRequestCommand request, CancellationToken cancellationToken)
    {
        var serviceRequest = mapper.Map<ServiceRequest>(request);

        foreach (var file in request.Attachments)
        {
            var (url, publicId) = await cloudinaryService.UploadFileAsync(file);

            var attachment = new Attachment
            {
                Id = Guid.NewGuid(),
                FileName = file.FileName,
                FilePath = url,                  
                ContentType = file.ContentType,
                UploadedAt = DateTime.UtcNow,
                PublicId = publicId,                 
                ServiceRequestId = serviceRequest.Id 
            };

            serviceRequest.Attachments.Add(attachment);
        }


        var newServiceRequest = await serviceRequestRepository.CreateAsync(serviceRequest);
        return mapper.Map<ServiceRequestDto>(newServiceRequest);
    }
}
