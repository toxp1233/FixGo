using gizmogeo.Application.ServiceRequests.Dtos;
using MediatR;

namespace gizmogeo.Application.ServiceRequests.Querys.GetServiceById;

public record GetServiceByIdQuery(Guid Id) : IRequest<ServiceRequestDto>;
