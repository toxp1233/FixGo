using AutoMapper;
using gizmogeo.Application.AcceptedRequests.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Exceptions;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.AcceptedRequests.Commands.CreateAcceptedRequest;

public class CreateAcceptedRequestCommandHandler(
    IServiceRequestRepository serviceRequestRepository,
    IAcceptedRequestsRepository acceptedRequestsRepository,
    IMapper mapper,
    ICloudinaryService cloudinaryService
    ) 
    : IRequestHandler<CreateAcceptedRequestCommand, AcceptedRequestByUserDto>
{
    public async Task<AcceptedRequestByUserDto> Handle(CreateAcceptedRequestCommand request, CancellationToken cancellationToken)
    {
        var serviceRequested = await serviceRequestRepository.GetByIdAsync(request.ServiceRequestId)
            ?? throw new NotFoundException(nameof(ServiceRequest), request.ServiceRequestId.ToString());

        var acceptedRequest = new AcceptedRequest
        {
            Id = Guid.NewGuid(),
            ServiceRequestId = serviceRequested.Id,
            Message = request.Message,
            EstimatedCost = request.EstimatedCost,
            Status = Domain.Enums.RequestStatus.Pending,
            RespondedAt = DateTime.UtcNow,
            Category = serviceRequested.Category,
            UserId = request.UserId,
        };


        if (request.Attachments != null && request.Attachments.Any())
        {
            foreach (var file in request.Attachments)
            {
                var (url, publicId) = await cloudinaryService.UploadFileAsync(file);
                var attachment = new Attachment
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    FilePath = url, 
                    PublicId = publicId,
                    ContentType = file.ContentType,
                    UploadedAt = DateTime.UtcNow,
                    AcceptedRequestId = acceptedRequest.Id
                };

                acceptedRequest.Attachments?.Add(attachment);
            }
        }

        var newAcceptedRequest = await acceptedRequestsRepository.CreateAsync(acceptedRequest);
        return mapper.Map<AcceptedRequestByUserDto>(newAcceptedRequest);
    }
}
