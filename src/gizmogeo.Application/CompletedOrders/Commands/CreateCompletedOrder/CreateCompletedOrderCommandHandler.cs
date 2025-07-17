using AutoMapper;
using gizmogeo.Application.CompletedOrders.Commands;
using gizmogeo.Application.CompletedOrders.Dtos;
using gizmogeo.Domain.Entities;
using gizmogeo.Domain.Interfaces;
using MediatR;

namespace gizmogeo.Application.CompletedOrders.Handlers;

public class CreateCompletedOrderHandler(
    ICompletedOrderRepository completedOrderRepository,
    IUserRepository userRepository,
    ICloudinaryService cloudinaryService,
    IAcceptedRequestsRepository acceptedRequestsRepository,
    IMapper mapper
) : IRequestHandler<CreateCompletedOrderCommand, CompletedOrderDto>
{
    public async Task<CompletedOrderDto> Handle(CreateCompletedOrderCommand request, CancellationToken cancellationToken)
    {
        var completedOrder = mapper.Map<CompletedOrder>(request);

        var completedByUser = await userRepository.GetByIdAsync(request.CompletedByUserId);
        var acceptedRequest = await acceptedRequestsRepository.GetByIdWithServiceRequestAsync(request.AcceptedRequestId);

        completedOrder.CompletedByName = completedByUser?.Name ?? "Unknown";
        completedOrder.RequestedByName = acceptedRequest?.ServiceRequest?.User?.Name ?? "Unknown";

        if (request.Attachments != null && request.Attachments.Any())
        {
            foreach (var file in request.Attachments)
            {
                var(url, publicId) = await cloudinaryService.UploadFileAsync(file);
                var attachment = new Attachment
                {
                    Id = Guid.NewGuid(),
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FilePath = url,
                    PublicId = publicId,
                    UploadedAt = DateTime.UtcNow,
                    CompletedOrderId = completedOrder.Id
                };

                completedOrder.Attachments.Add(attachment);
            }
        }

        var createdCompletedOrder = await completedOrderRepository.CreateAsync(completedOrder);
        return mapper.Map<CompletedOrderDto>(createdCompletedOrder);
    }
}
