using gizmogeo.Application.ServiceRequests.Dtos;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Querys.GetAllServices;

public record GetAllServicesQuery : IRequest<IEnumerable<ServiceRequestDto>>;
