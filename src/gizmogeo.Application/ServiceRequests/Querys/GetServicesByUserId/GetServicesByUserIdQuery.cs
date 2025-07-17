using gizmogeo.Application.ServiceRequests.Dtos;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Querys.GetServicesByUserId;

public record GetServicesByUserIdQuery(Guid UserId) : IRequest<IEnumerable<ServiceRequestDto>>;
